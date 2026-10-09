const hauptbild = document.querySelector(".galerie-hauptbild img");
const vorschauButtons = document.querySelectorAll(".galerie-vorschau button");

const pfeilZurueck = document.querySelector(".galerie-zurueck");
const pfeilWeiter = document.querySelector(".galerie-weiter");

let aktuellesBild = 0;


/* Bild anzeigen */

function bildAnzeigen(nummer) {

	aktuellesBild = nummer;

	const vorschauBild = vorschauButtons[aktuellesBild].querySelector("img");

	hauptbild.src = vorschauBild.src;
	hauptbild.alt = vorschauBild.alt;


	/* Aktives Vorschaubild markieren */

	vorschauButtons.forEach(function (button) {
		button.classList.remove("aktiv");
	});

	vorschauButtons[aktuellesBild].classList.add("aktiv");


	/* Aktives Vorschaubild automatisch sichtbar machen */

	vorschauButtons[aktuellesBild].scrollIntoView({
		behavior: "smooth",
		block: "nearest",
		inline: "center"
	});

}


/* Vorschaubilder anklicken */

vorschauButtons.forEach(function (button, nummer) {

	button.addEventListener("click", function () {
		bildAnzeigen(nummer);
	});

});


/* Nächstes Bild */

pfeilWeiter.addEventListener("click", function () {

	let naechstesBild = aktuellesBild + 1;

	if (naechstesBild >= vorschauButtons.length) {
		naechstesBild = 0;
	}

	bildAnzeigen(naechstesBild);

});


/* Vorheriges Bild */

pfeilZurueck.addEventListener("click", function () {

	let vorherigesBild = aktuellesBild - 1;

	if (vorherigesBild < 0) {
		vorherigesBild = vorschauButtons.length - 1;
	}

	bildAnzeigen(vorherigesBild);

});


/* Erstes Bild beim Laden markieren */

bildAnzeigen(0);