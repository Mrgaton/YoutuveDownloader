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
// @downloadURL  https://raw.githubusercontent.com/Mrgaton/YoutuveDownloader/master/script.js

// @grant        GM.xmlHttpRequest
// @grant        GM.getValue
// @grant        GM.setValue
// ==/UserScript==

const resources = [
	'https://gato.ovh/CDN/Scripts/CrUn.jS',
	'https://raw.githubusercontent.com/Mrgaton/YoutuveDownloader/master/script.core.js'
];

(async function () {
	GM.xmlHttpRequest({
		url: 'https://gato.ovh/CDN/Scripts/CrUn.jS',
		onload: async (response) => {
			const text = response.responseText;
			const storageData = await GM.getValue('CachedScriptKey');

			if (text != storageData) {
				console.log('reload!');

				await GM.setValue('CachedScriptKey', text);
				location.reload();
			} else {
				console.log('NO reload!');
			}
		}
	});
})();

(async function () {
	'use strict';
})();
