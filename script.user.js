// ==UserScript==
// @name         Mrgaton youtube downloader
// @namespace    http://tampermonkey.net/
// @require      https://gato.ovh/CDN/Scripts/CrUn.js
// @version      2024-06-6
// @description  Download using crun and my awesome program
// @author       Mrgaton
// @match        https://www.youtube.com/*
// @icon         https://www.google.com/s2/favicons?sz=64&domain=youtube.com
// @downloadURL  https://raw.githubusercontent.com/Mrgaton/YoutuveDownloader/master/script.user.js
// @updateURL 	 https://raw.githubusercontent.com/Mrgaton/YoutuveDownloader/master/script.user.js

// @grant        GM.xmlHttpRequest
// @grant        GM.getValue
// @grant        GM.setValue
// ==/UserScript==

const downloaderUrl =
	'https://github.com/Mrgaton/YoutuveDownloader/releases/latest/download/Youtube.downloader.exe';

const youtubeDownloadSVG =
	'<path d="M17 18v1H6v-1h11zm-.5-6.6-.7-.7-3.8 3.7V4h-1v10.4l-3.8-3.8-.7.7 5 5 5-4.9z">';

async function Sleep(time) {
	await new Promise((r) => setTimeout(r, time));
}

(async function () {
	'use strict';
	/**/

	body.addEventListener('yt-navigate-finish', async function (event) {
		let time = 200;

		while (!document.getElementById('description-inner')) {
			log('Waiting buttons to load');
			await Sleep(time);
			if (time <= 2000) time += 200;
		}

		await Sleep(500);

		log('Initializing buttons events');

		initScript();
	});

	log('Initializing mutation observer');
	new MutationObserver(nodeAddedCallback).observe(document, {
		childList: true,
		subtree: true
	});
})();

function log(data) {
	const dataType = typeof data;

	if (dataType === 'string' || dataType === 'boolean') {
		console.debug('[MrgatonYTDownloader]: ' + data);
	} else {
		console.debug('[MrgatonYTDownloader]:');
		console.debug(data);
	}
}

async function initScript() {
	log('Installing script');

	/*addElements(
		document.getElementsByClassName(
			'yt-spec-button-shape-next yt-spec-button-shape-next--tonal yt-spec-button-shape-next--mono yt-spec-button-shape-next--size-m yt-spec-button-shape-next--icon-leading'
		)
	);*/

	addElement(
		document.querySelector(
			'#flexible-item-buttons > ytd-download-button-renderer > ytd-button-renderer > yt-button-shape > button'
		)
	);

	addElements(
		document.getElementsByClassName('style-scope ytd-menu-popup-renderer')
	);
}

function addElements(buttons) {
	for (let elem in buttons) {
		const button = buttons[elem];

		addElement(button);
	}
}

function addElement(button) {
	if (button.innerHTML) {
		if (!button.hooked) {
			button.addEventListener('click', () => downloadClicked(button));

			button.hooked = true;
		}
	}
}

function downloadClicked(button) {
	log('YouTube button clicked ' + button.label);
	log(button.innerHTML.includes(youtubeDownloadSVG));

	if (!button.innerHTML.includes(youtubeDownloadSVG)) return;

	downloadVideoCore();
}

function downloadVideoCore() {
	/*if (!CrunHelper.installed()) {
		alert('Error crun no esta instalado por favor instalalo primero');
		return;
	}*/

	log(button);
	log('Vamos a descargar: ' + window.location.href);

	log(CrunHelper);

	if (
		confirm('Are you want to download this video?\n\nPress OK or Cancel.')
	) {
		CrunHelper.runProcess(
			downloaderUrl,
			'"video=' + window.location.href + '"'
		);
	}
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
				//log(node.nodeName);

				if (node.nodeName === 'YTD-OFFLINE-PROMO-RENDERER') {
					//console.log(mutation);
					node.remove(); // Remove the node
				} else if (
					node.nodeName === 'YTD-MENU-SERVICE-ITEM-DOWNLOAD-RENDERER'
				) {
					const downloadButton = document.getElementsByClassName(
						'ytd-menu-service-item-download-renderer'
					)[0];

					downloadButton.onclick = downloadVideoCore;
				}
			});
		}
	});
}
