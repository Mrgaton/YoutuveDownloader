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
					location.reload(true);
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
