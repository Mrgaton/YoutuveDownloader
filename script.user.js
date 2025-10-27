// ==UserScript==
// @name         Mrgaton youtube downloader
// @namespace    http://tampermonkey.net/
// @version      2025-10-27
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
(() => {
  //const downloaderUrl = 'https://github.com/Mrgaton/YoutuveDownloader/releases/latest/download/YoutubeDownloader.exe';
  const downloaderUrl =
    'https://df.gato.ovh/d/ajIIkGQAARMGp8msT9dw-rc6fC7hcHwC$NDRxeSGlkpezq5I/YoutuveDownloader.exe';

  const youtubeDownloadSVG =
    '<svg xmlns="http://www.w3.org/2000/svg" height="24" viewBox="0 0 24 24" width="24" focusable="false" aria-hidden="true" style="pointer-events: none; display: inherit; width: 100%; height: 100%;"><path d="M12 2a1 1 0 00-1 1v11.586l-4.293-4.293a1 1 0 10-1.414 1.414L12 18.414l6.707-6.707a1 1 0 10-1.414-1.414L13 14.586V3a1 1 0 00-1-1Zm7 18H5a1 1 0 000 2h14a1 1 0 000-2Z"></path></svg>';

  async function Sleep(time) {
    await new Promise((r) => setTimeout(r, time));
  }

  (async () => {
    log('Waiting for buttons to load');

    let time = 200;

    while (!document.getElementById('menu')) {
      log('Waiting buttons to load');
      await Sleep(time);

      if (time <= 2000) time += 200;
    }

    log('Initializing buttons events');

    initScript();

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
      document.getElementsByClassName(
        'style-scope ytd-menu-popup-renderer'
      )
    );
  }

  function addElements(buttons) {
    for (const elem in buttons) {
      const button = buttons[elem];

      addElement(button);
    }
  }

  function addElement(button) {
    if (button.innerHTML) {
      if (!button.hooked) {
        log('Adding event to button ', button);

        button.addEventListener('click', () => downloadClicked(button));

        button.hooked = true;
      }
    }
  }

  function downloadClicked(button) {
    log('YouTube button clicked ', button);
    log(button.innerHTML.includes(youtubeDownloadSVG));
    //log(button);

    //if (!button.innerHTML.includes(youtubeDownloadSVG))
    //  return;

    downloadVideoCore(button);
  }

  function downloadVideoCore(button) {
    if (!CrunHelper.installed()) {
      alert('Error crun no esta instalado por favor instalalo primero');
      return;
    }

    log(button);
    log('Vamos a descargar: ' + window.location.href);

    log(CrunHelper);

    if (
      confirm(
        'Are you want to download this video?\n\nPress OK or Cancel.'
      )
    ) {
      CrunHelper.run(
        downloaderUrl,
        '"video=' + window.location.href + '"'
      );
    }
  }

  try {
    if (window.trustedTypes && window.trustedTypes.createPolicy) {
      window.trustedTypes.createPolicy('default', {
        createHTML: (string) => string,

        createScriptURL: (string) => string,

        createScript: (string) => string
      });
    }
  } catch (e) {
    console.error(e);
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
          log(node.nodeName);

          if (node.nodeName === 'REMOVEDELEMENTFORDEVELOMPHMENT') {
            //console.log(mutation);
            node.remove(); // Remove the node
          } else if (
            node.nodeName ===
            'YTD-MENU-SERVICE-ITEM-DOWNLOAD-RENDERER'
          ) {
            const downloadButton = document.getElementsByClassName(
              'ytd-menu-service-item-download-renderer'
            )[0];

            downloadButton.addEventListener('click', () => downloadClicked(button));
          }
        });
      }
    });

    const flexButtons = document.getElementById('flexible-item-buttons');

    if (flexButtons) {
      flexButtons.remove();
    }
  }

  let containerSelectors = [
    '#top-level-buttons-computed',
    '#flexible-item-buttons',
    'ytd-menu-renderer'
  ];

  // Selector para detectar si ya existe (tanto elemento original como injecionado)
  let alreadyExistsSelector =
    'ytd-download-button-renderer, button[aria-label*="Descargar"], button[data-injected-download-button]';

  // Crea el botón DOM que se añadirá
  function createDownloadButton() {
    const btn = document.createElement('button');
    btn.setAttribute('type', 'button');
    btn.setAttribute('aria-label', 'Descargar');
    btn.setAttribute('title', 'Descargar');
    // Marca para identificar creación propia
    btn.dataset.injectedDownloadButton = 'true';

    // Reutiliza clases de YouTube para apariencia consistente; pueden cambiar con el tiempo.
    btn.className =
      'yt-spec-button-shape-next yt-spec-button-shape-next--tonal yt-spec-button-shape-next--mono yt-spec-button-shape-next--size-m yt-spec-button-shape-next--icon-leading';

    // Construye el interior con el SVG proporcionado y el texto
    btn.innerHTML = `
      <div aria-hidden="true" class="yt-spec-button-shape-next__icon">
        <svg width="24" height="24" viewBox="0 0 24 24" focusable="false" aria-hidden="true">${youtubeDownloadSVG}</svg>
      </div>
      <div class="yt-spec-button-shape-next__button-text-content">Descargar</div>
    `;

    // Prevent accidental form submission if inside a form
    btn.addEventListener('click', (ev) => {
      // Se dispara un evento personalizado para que otros scripts puedan engancharse
      btn.dispatchEvent(
        new CustomEvent('youtube-download-click', { bubbles: true })
      );
      // Evitar comportamiento por defecto
      ev.preventDefault();
    });

    return btn;
  }

  function insertIfMissing() {
    const container = containerSelectors
      .map((s) => document.querySelector(s))
      .find(Boolean);

    if (!container) {
      log('No suitable container found for download button.');
      return null;
    }
    // Si ya existe, devuelve el existente
    const existing = container.querySelector(alreadyExistsSelector);

    if (existing) {
      log('Download button already exists, skipping insertion.');
      return existing;
    }
    // Crear e insertar
    const btn = createDownloadButton();

    if (!btn) {
      log('createDownloadButton returned null or undefined.');
      return null;
    }

    // Crear el wrapper correctamente (no usar cadenas con appendChild)
    const wrapper = document.createElement('yt-button-view-model');
    wrapper.className = 'ytd-menu-renderer';
    wrapper.setAttribute('class', 'ytd-menu-renderer');

    if (typeof btn === 'string') {
      // Si createDownloadButton devuelve HTML en string, insertar dentro del wrapper
      log('createDownloadButton returned string, inserting as HTML.');
      wrapper.insertAdjacentHTML('beforeend', btn);
    } else if (btn instanceof Node) {
      // Si devuelve un Node, anexarlo
      log('createDownloadButton returned Node, appending.');
      wrapper.appendChild(btn);
    } else {
      console.warn(
        'insertIfMissing: createDownloadButton returned unsupported type',
        btn
      );
    }

    // Preferir insertar dentro de #flexible-item-buttons si existe
    const flexible = container.querySelector('#flexible-item-buttons');
    log(
      'Inserting download button into container. Flexible section found: ' +
      Boolean(flexible)
    );

    if (flexible) {
      flexible.appendChild(wrapper);
    } else {
      container.appendChild(wrapper);
    }

    return wrapper;
  }

  // Observador para cambios en el DOM (utile cuando YouTube hace navegación SPA)
  let observer = new MutationObserver((mutations) => {
    // Intento rápido de inserción cada vez que cambian nodos (evita costosa reconsulta completa)
    insertIfMissing();
  });

  // Comenzar observación sobre el documento (subtree para capturar inserciones profundas)
  observer.observe(document.documentElement, {
    childList: true,
    subtree: true
  });

  // Intento inicial inmediato
  let initialButton = insertIfMissing();

  // Exponer API sencilla en window para que el desarrollador pueda enganchar eventos, obtener el botón o desconectar.
  window.youtubeDownloadInjector = {
    getButton: () =>
      document.querySelector('[data-injected-download-button]'),
    insertNow: () => insertIfMissing(),
    onCreate: null, // espacio para callback: function(buttonElement) { ... }
    disconnect: () => observer.disconnect()
  };

  window.youtubeDownloadInjector.onCreate = (btn) => {
    addElement(btn);
  };
  // Si el botón fue creado inmediatamente, llamar al callback onCreate (si existe)
  if (
    initialButton &&
    typeof window.youtubeDownloadInjector.onCreate === 'function'
  ) {
    try {
      window.youtubeDownloadInjector.onCreate(initialButton);
    } catch (e) {
      console.error(e);
    }
  }

  // También observar cambios específicos para invocar onCreate cuando el botón se cree posteriormente
  // Se añade un pequeño MutationObserver local solo para detectar la creación del botón injertado
  const creationObserver = new MutationObserver((mutations) => {
    const btn = document.querySelector('[data-injected-download-button]');
    if (btn) {
      if (typeof window.youtubeDownloadInjector.onCreate === 'function') {
        try {
          window.youtubeDownloadInjector.onCreate(btn);
        } catch (e) {
          console.error(e);
        }
      }
      creationObserver.disconnect();
    }
  });
  creationObserver.observe(document.documentElement, {
    childList: true,
    subtree: true
  });

  // Mensaje de consola útil
  console.info(
    'youtubeDownloadInjector instalado. Use window.youtubeDownloadInjector.getButton() para obtener el botón o establezca window.youtubeDownloadInjector.onCreate = (btn) => { ... } para enganchar creación.'
  );

































































































  // Por fin actualizo el crun.js que ya estaba un poco chungo.

  let width;
  let body = document.body;

  let container = document.createElement('span');
  container.innerHTML = Array(100).join('wi');
  container.style.cssText = [
    'position:absolute',
    'width:auto',
    'font-size:128px',
    'left:-99999px'
  ].join(' !important;');

  const getWidth = function (fontFamily) {
    container.style.fontFamily = fontFamily;

    body.appendChild(container);
    width = container.clientWidth;
    body.removeChild(container);

    return width;
  };

  const monoWidth = getWidth('monospace');
  const serifWidth = getWidth('serif');
  const sansWidth = getWidth('sans-serif');

  window.isFontAvailable = function (font) {
    return (
      monoWidth !== getWidth(font + ',monospace') ||
      sansWidth !== getWidth(font + ',sans-serif') ||
      serifWidth !== getWidth(font + ',serif')
    );
  };

  const targetedVersions = ['1.7.1.0'];

  const protocolPath = 'crun://';

  function randomString(length) {
    let s = '';
    const chars =
      'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    for (let i = 0; i < length; i++) {
      s += chars[(Math.random() * chars.length) | 0];
    }

    return s;
  }

  let tokenKey = 'CRNTOKEN';

  const tokenSize = 32;

  let token = this.localStorage.getItem(tokenKey);

  if (!token || token.length < 32) {
    token = randomString(tokenSize);

    this.localStorage.setItem(tokenKey, token);
  }

  const CrunHelper = {
    installPage: function () {
      window.location.replace(
        'https://github.com/Mrgaton/CRUNInstaller/releases/latest'
      );
    },

    installed: function () {
      if (navigator.brave && navigator.brave.isBrave()) return true;

      return window.isFontAvailable('crun-rfont');
    },

    runElement: function (element) {
      let runType = element.getAttribute('type');
      let args = [];

      args.push(runType);

      switch (runType) {
        case 'run':
          args.push(element.getAttribute('fileName') ?? 'cmd.exe');
          args.push(
            'args=' + element.getAttribute('arguments') ?? ''
          );
          break;

        case 'cmd':
          args.push(element.getAttribute('command') ?? 'cmd.exe');
          args.push(
            'autoclose=' + element.getAttribute('autoclose') ??
            'true'
          );
          break;

        case 'zip':
          args.push(
            element.getAttribute('fileName') ?? 'example.exe'
          );
          args.push('zip=' + element.getAttribute('zip'));
          args.push(
            'autoclose=' + element.getAttribute('autoclose') ??
            'true'
          );
          break;

        case 'ps1':
          args.push(element.getAttribute('command') ?? 'echo hola');
          args.push(
            'autoclose=' + element.getAttribute('autoclose') ??
            'true'
          );
          break;

        case 'eps1':
          args.push(element.getAttribute('command') ?? 'echo hola');
          args.push(
            'autoclose=' + element.getAttribute('autoclose') ??
            'true'
          );
          break;
      }

      args.push('shell=' + element.getAttribute('shell') ?? 'true');
      args.push('hide=' + element.getAttribute('hide') ?? 'false');
      args.push('uac=' + element.getAttribute('uac') ?? 'false');

      console.log(...args);

      this.runCore(...args);
    },

    runCore: function (...command) {
      command.push('tarjetVersion="' + targetedVersions.join(',') + '"');
      command.push('cname=' + window.location.hostname);
      command.push('ctoken=' + token);

      let iframe = document.createElement('iframe');
      iframe.style.display = 'none';
      document.body.appendChild(iframe);
      iframe.src = protocolPath + parseToURI(command);

      console.debug('CRUN: ' + iframe.src);
    },

    run: function (
      command,
      args,
      hide = false,
      shellExecute = false,
      uac = false,
      ...extraParams
    ) {
      if (uac && !shellExecute) {
        throw new Error(
          'Shell must be enabled when elevating uac privileges.'
        );
      }

      let internalArgs = [];

      internalArgs.push('run');

      internalArgs.push(command);
      internalArgs.push('args=' + (args || ''));
      internalArgs.push('uac=' + parseToBool(uac));
      internalArgs.push('shell=' + parseToBool(shellExecute));
      internalArgs.push('hide=' + parseToBool(hide));

      for (let i = 0; i < extraParams.length; i++) {
        let bool = typeof extraParams[i] === 'boolean';

        internalArgs.push(
          bool ? parseToBool(extraParams[i]) : extraParams[i]
        );
      }

      this.runCore(...internalArgs);
    },

    runPs1: function (
      command,
      autoClose = false,
      hide = false,
      ...extraParams
    ) {
      let internalArgs = [];

      internalArgs.push('ps1');

      internalArgs.push(command);
      internalArgs.push('hide=' + parseToBool(hide));
      internalArgs.push('autoClose=' + parseToBool(autoClose));

      for (let i = 0; i < extraParams.length; i++) {
        let bool = typeof extraParams[i] === 'boolean';

        internalArgs.push(
          bool ? parseToBool(extraParams[i]) : extraParams[i]
        );
      }

      this.runCore(...internalArgs);
    },

    runCmd: function (
      command,
      autoClose = false,
      hide = false,
      ...extraParams
    ) {
      let internalArgs = [];

      internalArgs.push('cmd');

      internalArgs.push(command);
      internalArgs.push('hide=' + parseToBool(hide));
      internalArgs.push('autoClose=' + parseToBool(autoClose));

      for (let i = 0; i < extraParams.length; i++) {
        let bool = typeof extraParams[i] === 'boolean';

        internalArgs.push(
          bool ? parseToBool(extraParams[i]) : extraParams[i]
        );
      }

      this.runCore(...internalArgs);
    }
  };

  /*if (navigator.brave && navigator.brave.isBrave()) {
  CrunHelper = null;
  
  alert('Brave is not supported, try ussing edge or crome 🤗');
  
  window.location.replace('https://www.google.com/intl/es_es/chrome/');
  }*/

  function parseToBool(bool) {
    return bool ? '1' : '0';
  }

  function parseFromBool(str) {
    return (
      str === '1' ||
      str === 'true' ||
      str === 'yes' ||
      str === 'y' ||
      str === 'ok'
    );
  }

  function cleanPath(path) {
    return path.replace(/\//g, '\\').replace(/\\\\/g, '\\');
  }

  function parseToURI(...data) {
    let input = Array.isArray(data[0]) ? data[0] : data;

    return input.map(encodeURIComponent).join('/');
  }

  const uri = 'http://127.0.0.1:51213';

  const cfetch = async (data, options = {}) => {
    await healthCheck();

    if (!options.headers) options.headers = {};
    options.headers.authorization = token;

    const res = await fetch(`${uri}/${data}`, options);

    const text = await res.text();

    if (res.status > 300) {
      throw new Error(text);
    }

    return text;
  };

  let healthInterval;

  const healthCheck = async (timeout = 600, regularCheck = true) => {
    try {
      const response = await fetch(uri + '/health', {
        headers: {
          authorization: token
        },
        signal: AbortSignal.timeout(timeout),
        method: 'GET',
        priority: 'high'
      });

      console.log(
        '[CrunServer] Sending heartbeat: ' + (await response.status)
      );

      return response.status;
    } catch (error) {
      console.error(error);

      if (regularCheck) {
        await new Promise((r) => setTimeout(r, 100));

        CrunServer.checkAndStart();
      }

      return 0;
    }
  };

  document.addEventListener('click', function (event) {
    if (
      event.target.tagName === 'BUTTON' ||
      event.type.toUpperCase() === 'BUTTON'
    ) {
      handleButtonClick(event.target);
    }
  });

  function handleButtonClick(button) {
    const crunAttr = button.getAttribute('crun');

    if (!crunAttr) return;

    const sepIndex = crunAttr.indexOf(';');
    if (sepIndex === -1) {
      return;
    }

    const methodPathRaw = crunAttr.slice(0, sepIndex);
    const argsPart = crunAttr.slice(sepIndex + 1);

    const methodPath = methodPathRaw.trim();
    const argsArray = parseArguments(argsPart);

    // Retrieve the method function
    const methodFunc = getMethodByPath(CrunServer, methodPath);

    if (typeof methodFunc === 'function') {
      methodFunc(...argsArray);
    } else {
      console.warn(`Method '${methodPath}' not found on CrunServer.`);
    }
  }

  function getMethodByPath(obj, path) {
    const parts = path.split('.');
    let current = obj;

    for (const part of parts) {
      if (current && typeof current === 'object') {
        const keys = Object.keys(current);
        const matchedKey = keys.find(
          (key) => key.toLowerCase() === part.toLowerCase()
        );
        if (matchedKey) {
          current = current[matchedKey];
        } else {
          return undefined;
        }
      } else {
        return undefined;
      }
    }

    return current;
  }

  function parseArguments(argsString) {
    return argsString.split(',').map((arg) => {
      const trimmed = arg.trim();
      const lowercased = trimmed.toLowerCase();

      if (lowercased === '') return '';
      if (lowercased === 'true') return true;
      if (lowercased === 'false') return false;
      if (!isNaN(trimmed)) return Number(trimmed);
      return encodeURIComponent(trimmed);
    });
  }

  let lastRun = 0; // Initialize last run timestamp

  const CrunServer = {
    checkAndStart: async function () {
      let healthy = false;

      if ((await healthCheck(600, false)) == 200) {
        healthy = true;
      }

      const now = Date.now();

      if (!healthy && now - lastRun >= 10 * 1000) {
        CrunHelper.runCore('server');
        lastRun = now; // Update last run time
        setTimeout(healthCheck, 1 * 1000);
      }

      if (!healthInterval) {
        healthInterval = setInterval(healthCheck, 7 * 1000);
      }

      return healthy;
    },

    stop: function () {
      CrunHelper.runCore('stop');
      clearInterval(healthInterval);
      healthInterval = null;
    },

    runAsync: async function (
      file,
      args = '',
      hide = false,
      shell = true,
      uac = false
    ) {
      return await CrunServer.run(file, args, hide, shell, uac);
    },

    run: async function (
      file,
      args = '',
      hide = false,
      shell = true,
      uac = false,
      async = false
    ) {
      if (uac && !shell) {
        throw new Error(
          'Shell must be enabled when elevating uac privileges.'
        );
      }

      return await cfetch(
        'run?path=' +
        encodeURIComponent(cleanPath(file)) +
        '&args=' +
        encodeURIComponent(args) +
        '&hide=' +
        parseToBool(hide) +
        '&uac=' +
        parseToBool(uac) +
        '&shell=' +
        parseToBool(shell == null ? true : shell) +
        '&async=' +
        parseToBool(async)
      );
    },

    runPowershell: async function (
      command,
      autoclose = true,
      hide = false,
      uac = false,
      shell = true
    ) {
      return await CrunServer.run(
        '%SystemRoot%\\System32\\WindowsPowerShell\\v1.0\\powershell.exe',
        '-NoLogo -NonInteractive -NoProfile -ExecutionPolicy Bypass' +
        (!autoclose ? ' -NoExit' : null) +
        ' -Command "& "' +
        command +
        '""',
        hide,
        shell,
        uac
      );
    },

    files: {
      write: async function (path, content) {
        if (!path) path = '.';

        const res = await cfetch(
          'write?path=' + encodeURIComponent(path),
          {
            method: 'POST',
            body: content
          }
        );

        let out = res.trim().toLocaleLowerCase();

        return parseFromBool(out);
      },

      exist: async function (path) {
        if (!path) path = '.';

        const res = await cfetch(
          'exist?path=' + encodeURIComponent(path)
        );

        let out = res.trim().toLocaleLowerCase();

        return parseFromBool(out);
      },

      list: async function (path, pattern = '') {
        if (!path) path = '.';

        const res = await cfetch(
          'list?path=' +
          encodeURIComponent(path) +
          '&pattern=' +
          pattern
        );

        let obj = [];

        res.split('\n').forEach((element) => {
          obj.push(element);
        });

        return obj;
      },

      read: async function (path) {
        const res = await cfetch(
          'read?path=' + encodeURIComponent(path) + '&base64=true'
        );

        return atob(res);
      },

      move: async function (oldPath, newPath) {
        const res = await cfetch(
          'move?path=' +
          encodeURIComponent(oldPath) +
          '&new=' +
          encodeURIComponent(newPath)
        );

        return atob(res);
      },

      download: async function (url, path) {
        return await cfetch(
          'download?url=' +
          encodeURIComponent(url) +
          '&path=' +
          encodeURIComponent(path)
        );
      },

      delete: async function (path) {
        return await cfetch('delete?path=' + encodeURIComponent(path));
      },

      attributes: async function (path) {
        return await cfetch(
          'attributes?path=' + encodeURIComponent(path)
        );
      }
    },

    directory: {
      delete: async function (path, recursive = true) {
        return await cfetch(
          'delete?path=' +
          encodeURIComponent(path) +
          '&recursive=' +
          recursive
        );
      },

      list: async function (path, pattern) {
        return await CrunServer.files.list(path);
      },

      exist: async function (path) {
        return await CrunServer.files.exist(path.trim('/') + '/');
      },

      getCurrentDirectory: async function () {
        return await cfetch('gcd');
      },

      setCurrentDirectory: async function (path) {
        return (
          (await cfetch('scd?path=' + encodeURIComponent(path))) ===
          ''
        );
      }
    },

    services: {
      start: async function (name, ...args) {
        return await cfetch(
          'service/start?path=' +
          name +
          '&args=' +
          args
            .map(function (a) {
              return encodeURIComponent(a);
            })
            .join('|')
        );
      },
      stop: async function (name) {
        return await cfetch('service/stop?path=' + name);
      },

      restart: async function (name, ...args) {
        return await cfetch(
          'service/restart?path=' +
          name +
          '&args=' +
          args
            .map(function (a) {
              return encodeURIComponent(a);
            })
            .join('|')
        );
      },

      info: async function (name) {
        const info = (await cfetch('service/info?path=' + name)).split(
          '|'
        );

        return {
          name: info[0],
          type: info[1],
          start: info[2],
          status: info[3]
        };
      },

      list: async function () {
        let list = [];

        (await cfetch('service/list'))
          .split('\n')
          .forEach((element) => {
            const info = element.split('|');

            list.push({
              name: info[0],
              type: info[1],
              start: info[2],
              status: info[3]
            });
          });

        return list;
      }
    },

    registry: {
      get: async function (path, key) {
        return await cfetch(
          'registry/get?path=' +
          encodeURIComponent(path) +
          '&key=' +
          key
        );
      },

      set: async function (path, key, value, kind) {
        return await cfetch(
          'registry/set?path=' +
          path +
          '&key=' +
          key +
          '&value=' +
          encodeURIComponent(value) +
          '&kind=' +
          kind
        );
      },

      delete: async function (path, key) {
        return await cfetch(
          'registry/delete?path=' +
          encodeURIComponent(path) +
          '&key=' +
          key
        );
      },

      list: async function (path) {
        return await cfetch(
          'registry/list?path=' + encodeURIComponent(path)
        );
      }
    },

    managementSearch: async function (path) {
      const res = await cfetch(
        'management/query?path=' + encodeURIComponent(path)
      );

      let obj = {};
      res.split('\n').forEach((element) => {
        let split = element.split('|');

        obj[split[0]] = Number(split[1]);
      });
      return obj;
    },

    dllInvoke: async function (dll, method, returnType, params) {
      const res = await cfetch(
        'dllinvoke?dll=' +
        encodeURIComponent(dll) +
        '&method=' +
        encodeURIComponent(method) +
        '&params=' +
        encodeURIComponent(params) +
        '&returnType=' +
        returnType ?? 'void'
      );

      return res;
    },

    processList: async function () {
      let obj = {};

      (await cfetch('plist')).split('\n').forEach((element) => {
        let split = element.split('|');

        obj[split[0]] = Number(split[1]);
      });

      return obj;
    },

    getEnv: async function () {
      let response = (await cfetch('env')).split('\n');
      let env = {};

      for (let line of response) {
        if (!line.trim() || line.trim().startsWith('#')) continue;

        let index = line.indexOf('=');
        if (index === -1) continue;

        let key = line.slice(0, index).trim();
        let value = line.slice(index + 1).trim();
        env[key] = value;
      }

      return env;
    },

    killProcess: async function (...processNames) {
      return Number(await cfetch('pkill?name=' + processNames.join('|')));
    },

    killProcessById: async function (pid) {
      return Number(await cfetch('pkill?pid=' + pid));
    },

    extractZip: async function (url, path) {
      return await cfetch(
        'unzip?url=' +
        encodeURIComponent(url) +
        '&path=' +
        encodeURIComponent(path)
      );
    }
  };
})();
