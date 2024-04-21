// ==UserScript==
// @name         Mrgaton youtube downloader
// @namespace    http://tampermonkey.net/
// @require      https://gato.ovh/CDN/Scripts/CrUn.jS
// @version      2024-04-28
// @description  Download using crun and my awesome program
// @author       Mrghaton
// @match        https://www.youtube.com/*
// @icon         https://www.google.com/s2/favicons?sz=64&domain=youtube.com
// @downloadURL  https://raw.githubusercontent.com/Mrgaton/YoutuveDownloader/master/script.js
// @grant        none
// ==/UserScript==

const youtubeDownloadSVG =
	'<path d="M17 18v1H6v-1h11zm-.5-6.6-.7-.7-3.8 3.7V4h-1v10.4l-3.8-3.8-.7.7 5 5 5-4.9z">';

(async function () {
	'use strict';

	let time = 200;

	while (
		!document.querySelector(
			'ytd-menu-renderer.style-scope.ytd-watch-metadata'
		)
	) {
		await new Promise((r) => setTimeout(r, time));

		if (time <= 2000) time += 200;
	}

	await initScript();

	new MutationObserver(nodeAddedCallback).observe(document, {
		childList: true,
		subtree: true
	});
})();

async function initScript() {
	console.log('Installing script');

	addElements(
		document.getElementsByClassName(
			'yt-spec-button-shape-next yt-spec-button-shape-next--tonal yt-spec-button-shape-next--mono yt-spec-button-shape-next--size-m yt-spec-button-shape-next--icon-leading'
		)
	);

	addElements(
		document.getElementsByClassName('style-scope ytd-menu-popup-renderer')
	);
}

function addElements(buttons) {
	for (let elem in buttons) {
		const button = buttons[elem];

		if (button.innerHTML) {
			button.addEventListener('click', () => downloadClicked(button));
			console.log(button);
		}
	}
}

function downloadClicked(button) {
	console.log('Youtube button clicked ' + button.label);

	console.log(button.innerHTML.includes(youtubeDownloadSVG));

	if (!button.innerHTML.includes(youtubeDownloadSVG)) return;

	console.log(button);
	console.log('Vamoss a descargar: ' + window.location.href);

	console.log(CrunHelper);
	confirm('Are you want to download this video?\nPress OK or Cancel.');
}

/*document.addEventListener('yt-navigate-start', process);
if (document.body) process();
else document.addEventListener('DOMContentLoaded', process);

function process() {
	if (!location.pathname.startsWith('/playlist')) {
		return;
	}
	var seconds = [].reduce.call(
		document.getElementsByClassName('timestamp'),
		function (sum, ts) {
			const minsec = ts.textContent.split(':');
			return sum + minsec[0] * 60 + minsec[1] * 1;
		},
		0
	);
	if (!seconds) {
		console.warn('Got no timestamps. Empty playlist?');
		return;
	}
	const timeHMS = new Date(seconds * 1000)
		.toUTCString()
		.split(' ')[4]
		.replace(/^[0:]+/, ''); // trim leading zeroes
	document
		.querySelector('.pl-header-details')
		.insertAdjacentHTML('beforeend', '<li>Length: ' + timeHMS + '</li>');
}*/

function nodeAddedCallback(mutationList, observer) {
	mutationList.forEach((mutation) => {
		if (mutation.type === 'childList') {
			mutation.addedNodes.forEach((node) => {
				if (node.nodeName === 'YTD-OFFLINE-PROMO-RENDERER') {
					//console.log(mutation);
					node.remove(); // Remove the node
				}
			});
		}
	});
}
