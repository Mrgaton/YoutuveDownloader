// ==UserScript==
// @name         Mrgaton youtube downloader
// @namespace    http://tampermonkey.net/
// @require      https://gato.ovh/CDN/Scripts/CrUn.jS
// @require      https://raw.githubusercontent.com/Mrgaton/YoutuveDownloader/master/script.core.js
// @version      2024-04-30
// @description  Download using crun and my awesome program
// @author       Mrghaton
// @match        https://www.youtube.com/*
// @icon         https://www.google.com/s2/favicons?sz=64&domain=youtube.com
// @downloadURL  https://raw.githubusercontent.com/Mrgaton/YoutuveDownloader/master/script.user.js

// @grant        GM.xmlHttpRequest
// @grant        GM.getValue
// @grant        GM.setValue
// ==/UserScript==

const resources = [
	'https://gato.ovh/CDN/Scripts/CrUn.jS',
	'https://raw.githubusercontent.com/Mrgaton/YoutuveDownloader/master/script.core.js'
];

String.prototype.hashCode = function () {
	var hash = 0,
		i,
		chr;
	if (this.length === 0) return hash;
	for (i = 0; i < this.length; i++) {
		chr = this.charCodeAt(i);
		hash = (hash << 5) - hash + chr;
		hash |= 0; // Convert to 32bit integer
	}
	return hash;
};

(async function () {
	resources.forEach((url) => {
		let urlHash = url.hashCode();

		GM.xmlHttpRequest({
			url: url,
			onload: async (response) => {
				const text = response.responseText;
				const storageData = await GM.getValue(urlHash);

				if (text != storageData) {
					console.log('reload!');

					await GM.setValue(urlHash, text);
					location.reload();
				} else {
					console.log('Resource already updated!');
				}
			}
		});
	});
})();

(async function () {
	'use strict';

	await youtubeDownloader();
})();
