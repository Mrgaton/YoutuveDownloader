// ==UserScript==
// @name         Mrgaton youtube downloader
// @namespace    http://tampermonkey.net/
// @version      2024-04-20
// @description  Download using crun and my awesome program
// @author       Mrghaton
// @match        https://www.youtube.com/watch?v=*
// @icon         https://www.google.com/s2/favicons?sz=64&domain=youtube.com
// @downloadURL  https://github.com/Mrgaton/YoutuveDownloader/raw/master/script.js
// @grant        none
// ==/UserScript==

const youtubeDownloadSVG =
	'<path d="M17 18v1H6v-1h11zm-.5-6.6-.7-.7-3.8 3.7V4h-1v10.4l-3.8-3.8-.7.7 5 5 5-4.9z">';

(function () {
	'use strict';
	//window.addEventListener('load', (event) => {
	let buttons = document.getElementsByClassName(
		'yt-spec-button-shape-next yt-spec-button-shape-next--tonal yt-spec-button-shape-next--mono yt-spec-button-shape-next--size-m yt-spec-button-shape-next--icon-leading'
	);

	for (let elem in buttons) {
		const button = buttons[elem];

		if (button.innerHTML) {
			button.addEventListener('click', () => downloadClicked(button));
			console.log(button);
		}
	}

	//});
})();

function downloadClicked(button) {
	console.log('AHHH clickaddo');

	if (!button.innerHTML.includes(youtubeDownloadSVG)) return;

	document.getElementsByClassName(
		'style-scope ytd-popup-container'
	)[4].style.display = 'none';
    
	console.log(button);
}

document.addEventListener('yt-navigate-start', process);
// Choose a different event depending on when you want to apply the change
// document.addEventListener('yt-navigate-finish', process);

if (document.body) process();
else document.addEventListener('DOMContentLoaded', process);

function process() {
	if (!location.pathname.startsWith('/playlist')) {
		return;
	}
	var seconds = [].reduce.call(
		document.getElementsByClassName('timestamp'),
		function (sum, ts) {
			var minsec = ts.textContent.split(':');
			return sum + minsec[0] * 60 + minsec[1] * 1;
		},
		0
	);
	if (!seconds) {
		console.warn('Got no timestamps. Empty playlist?');
		return;
	}
	var timeHMS = new Date(seconds * 1000)
		.toUTCString()
		.split(' ')[4]
		.replace(/^[0:]+/, ''); // trim leading zeroes
	document
		.querySelector('.pl-header-details')
		.insertAdjacentHTML('beforeend', '<li>Length: ' + timeHMS + '</li>');
}