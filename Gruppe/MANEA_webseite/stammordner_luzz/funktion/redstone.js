const post = document.querySelector("#post");
const sheriff = document.querySelector("#sheriff");
const saloon = document.querySelector("#saloon");
const funfact = document.querySelector("#funfact");
const funfactTitel = funfact.querySelector("h3");
const funfactText = funfact.querySelector("p");

post.addEventListener("click", function () {
	funfactTitel.textContent = "Post für Redstone!";
	funfactText.textContent = "Mit Postkutschen reisten Briefe, Pakete und sogar Fahrgäste von Ort zu Ort. Die Fahrt konnte ziemlich lange dauern. Ein ungeduldiges Warten am Briefkasten hätte also wenig gebracht.";

	funfact.hidden = false;
});

sheriff.addEventListener("click", function () {
	funfactTitel.textContent = "Beim Sheriff";
	funfactText.textContent = "Der Sheriff sorgte in Redstone für Recht und Ordnung. In seinem Büro gab es oft auch ein kleines Gefängnis mit nur wenigen Zellen. Gemütlich war es dort bestimmt nicht – und Zimmerservice gab es auch keinen.";

	funfact.hidden = false;
});

saloon.addEventListener("click", function () {
	funfactTitel.textContent = "Willkommen im Saloon!";
	funfactText.textContent = "Im Saloon wurde nicht nur getrunken. Hier wurde gegessen, Karten gespielt und über die neuesten Geschichten aus der Stadt geredet. Langweilig wurde es in Redstone also bestimmt nicht.";

	funfact.hidden = false;
});

document.addEventListener("click", function (event) {
	if (!funfact.hidden &&
		!funfact.contains(event.target) &&
		!post.contains(event.target) &&
		!sheriff.contains(event.target) &&
		!saloon.contains(event.target)) {

		funfact.hidden = true;
	}
});