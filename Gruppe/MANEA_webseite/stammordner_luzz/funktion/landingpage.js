(function () {
	"use strict";

	document.documentElement.classList.add("js");

	const story = document.querySelector("#ScrollStory");

	if (!story) {
		return;
	}

	const frames = Array.from(story.querySelectorAll(".story-frame"));
	const scrollHint = document.querySelector("#ScrollHinweis");
	const progressElement = document.querySelector("#IntroFortschritt");
	const logo = document.querySelector("#LandingLogo");
	const reducedMotion = window.matchMedia("(prefers-reduced-motion: reduce)");

	/*
		Die Werte entsprechen den festgelegten Abschnitten:
		0–12, 12–25, 25–32, 32–48, 48–53,
		53–72, 72–82, 82–91 und 91–100 Prozent.
	*/
	const segmentStops = [0, 0.12, 0.25, 0.32, 0.48, 0.53, 0.72, 0.82, 0.91, 1];
	const transitionWidths = [0.022, 0.025, 0.014, 0.022, 0.012, 0.03, 0.025, 0.024];
	const zoomRanges = [
		[1.018, 1.035],
		[1.025, 1.045],
		[1.03, 1.018],
		[1.02, 1.105],
		[1, 1],
		[1.02, 1.075],
		[1.015, 1.04],
		[1.02, 1.06],
		[1, 1.01]
	];

	let animationFrame = 0;

	function clamp(value, minimum, maximum) {
		return Math.min(Math.max(value, minimum), maximum);
	}

	function smootherStep(start, end, value) {
		if (start === end) {
			return value < start ? 0 : 1;
		}

		const normalized = clamp((value - start) / (end - start), 0, 1);
		return normalized * normalized * normalized *
			(normalized * (normalized * 6 - 15) + 10);
	}

	function getFrameOpacity(frameIndex, progress) {
		let opacity = 1;

		if (frameIndex > 0) {
			const startBoundary = segmentStops[frameIndex];
			const halfFade = transitionWidths[frameIndex - 1] / 2;
			opacity *= smootherStep(
				startBoundary - halfFade,
				startBoundary + halfFade,
				progress
			);
		}

		if (frameIndex < frames.length - 1) {
			const endBoundary = segmentStops[frameIndex + 1];
			const halfFade = transitionWidths[frameIndex] / 2;
			opacity *= 1 - smootherStep(
				endBoundary - halfFade,
				endBoundary + halfFade,
				progress
			);
		}

		return opacity;
	}

	function getLocalProgress(frameIndex, progress) {
		const start = segmentStops[frameIndex];
		const end = segmentStops[frameIndex + 1];
		return clamp((progress - start) / (end - start), 0, 1);
	}

	function render(progress) {
		frames.forEach(function (frame, frameIndex) {
			const localProgress = getLocalProgress(frameIndex, progress);
			const zoom = zoomRanges[frameIndex];
			const scale = zoom[0] + (zoom[1] - zoom[0]) * localProgress;
			let movementX = 0;
			let movementY = 0;

			/* Kurzer Rückstoß während des Schusses. */
			if (frameIndex === 2) {
				const recoil = Math.sin(localProgress * Math.PI);
				movementX = -5 * recoil;
				movementY = 2 * recoil;
			}

			frame.style.opacity = getFrameOpacity(frameIndex, progress).toFixed(4);
			frame.style.setProperty("--frame-scale", scale.toFixed(4));
			frame.style.setProperty("--frame-x", movementX.toFixed(2) + "px");
			frame.style.setProperty("--frame-y", movementY.toFixed(2) + "px");
		});

		const flashIn = smootherStep(0.475, 0.495, progress);
		const flashOut = 1 - smootherStep(0.515, 0.54, progress);
		const flashOpacity = flashIn * flashOut * 0.5;
		story.style.setProperty("--flash-opacity", flashOpacity.toFixed(4));

		if (scrollHint) {
			scrollHint.style.opacity = String(1 - smootherStep(0.025, 0.075, progress));
		}

		if (progressElement) {
			progressElement.value = Math.round(progress * 100);
		}

		if (logo) {
			const logoProgress = smootherStep(0.915, 0.97, progress);
			const logoScale = 0.9 + logoProgress * 0.1;
			const logoIsActive = progress >= 0.955;

			logo.style.opacity = logoProgress.toFixed(4);
			logo.style.transform =
				"translate(-50%, -46%) scale(" + logoScale.toFixed(4) + ")";
			logo.classList.toggle("is-active", logoIsActive);
			logo.tabIndex = logoIsActive ? 0 : -1;
			logo.setAttribute("aria-hidden", logoIsActive ? "false" : "true");
		}
	}

	function calculateProgress() {
		const storyTop = story.getBoundingClientRect().top;
		const scrollDistance = Math.max(story.offsetHeight - window.innerHeight, 1);
		return clamp(-storyTop / scrollDistance, 0, 1);
	}

	function update() {
		animationFrame = 0;

		if (reducedMotion.matches) {
			render(1);
			return;
		}

		render(calculateProgress());
	}

	function requestUpdate() {
		if (!animationFrame) {
			animationFrame = window.requestAnimationFrame(update);
		}
	}

	window.addEventListener("scroll", requestUpdate, { passive: true });
	window.addEventListener("resize", requestUpdate);

	if (typeof reducedMotion.addEventListener === "function") {
		reducedMotion.addEventListener("change", requestUpdate);
	} else {
		reducedMotion.addListener(requestUpdate);
	}

	requestUpdate();
}());
