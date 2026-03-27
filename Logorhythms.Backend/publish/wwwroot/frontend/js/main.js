(function () {
  const prefix = /\/pages\//.test(window.location.pathname) ? "../" : "";

  function loadComponent(id, file) {
    return fetch(file)
      .then(function (res) {
        if (!res.ok) throw new Error("Failed to load " + file);
        return res.text();
      })
      .then(function (html) {
        var el = document.getElementById(id);
        if (el) el.innerHTML = html;
      });
  }

  function setNavHrefs() {
    var inPages = /\/pages\//.test(window.location.pathname);
    var map = {
      home: inPages ? "../index.html" : "index.html",
      about: inPages ? "about.html" : "pages/about.html",
      mandate: inPages ? "mandate.html" : "pages/mandate.html",
      projects: inPages ? "projects.html" : "pages/projects.html",
      profile: inPages ? "profile.html" : "pages/profile.html",
      "chief-executive": inPages
        ? "chief-executive.html"
        : "pages/chief-executive.html",
      history: inPages ? "history.html" : "pages/history.html",
      organogram: inPages ? "organogram.html" : "pages/organogram.html",
      "board-directors": inPages
        ? "board-directors.html"
        : "pages/board-directors.html",
      "executive-management": inPages
        ? "executive-management.html"
        : "pages/executive-management.html",
      careers: inPages ? "careers.html" : "pages/careers.html",
      sectors: inPages ? "sectors.html" : "pages/sectors.html",
      internet: inPages ? "internet.html" : "pages/internet.html",
      broadcasting: inPages ? "broadcasting.html" : "pages/broadcasting.html",
      postal: inPages ? "postal.html" : "pages/postal.html",
      telecommunications: inPages
        ? "telecommunications.html"
        : "pages/telecommunications.html",
      legislation: inPages ? "legislation.html" : "pages/legislation.html",
      "nrfp": inPages
        ? "national-radio-frequency-plan.html"
        : "pages/national-radio-frequency-plan.html",
      "spectrum-planning": inPages
        ? "radio-spectrum-planning.html"
        : "pages/radio-spectrum-planning.html",
      "numbering-plan": inPages ? "numbering-plan.html" : "pages/numbering-plan.html",
      "bw-cctld": inPages ? "bw-cctld.html" : "pages/bw-cctld.html",
      "bw-cirt": inPages ? "bw-cirt.html" : "pages/bw-cirt.html",
      "electronic-evidence": inPages
        ? "electronic-evidence.html"
        : "pages/electronic-evidence.html",
      "electronic-transactions": inPages
        ? "electronic-transactions.html"
        : "pages/electronic-transactions.html",
      complaints: inPages ? "complaints.html" : "pages/complaints.html",
      "consumer-education": inPages
        ? "consumer-education.html"
        : "pages/consumer-education.html",
      "registering-complaints": inPages
        ? "registering-complaints.html"
        : "pages/registering-complaints.html",
      licensing: inPages ? "licensing.html" : "pages/licensing.html",
      documents: inPages ? "documents.html" : "pages/documents.html",
      "draft-documents": inPages
        ? "draft-documents.html"
        : "pages/draft-documents.html",
      "ict-licensing-framework": inPages
        ? "ict-licensing-framework.html"
        : "pages/ict-licensing-framework.html",
      "itu-workshop": inPages ? "itu-workshop.html" : "pages/itu-workshop.html",
      asms: inPages ? "asms-webcp.html" : "pages/asms-webcp.html",
      "type-approval": inPages ? "type-approval.html" : "pages/type-approval.html",
      "licence-verification": inPages
        ? "licence-verification.html"
        : "pages/licence-verification.html",
      media: inPages ? "media.html" : "pages/media.html",
      "news-events": inPages ? "news-events.html" : "pages/news-events.html",
      speeches: inPages ? "speeches.html" : "pages/speeches.html",
      tenders: inPages ? "tenders.html" : "pages/tenders.html",
      "register-bw": inPages ? "register-bw.html" : "pages/register-bw.html",
      contact: inPages ? "contact.html" : "pages/contact.html",
      dashboard: inPages ? "dashboard.html" : "pages/dashboard.html",
    };
    document.querySelectorAll("[data-nav]").forEach(function (el) {
      var key = el.getAttribute("data-nav");
      if (map[key]) el.setAttribute("href", map[key]);
    });
  }

  function setAssetPaths() {
    var inPages = /\/pages\//.test(window.location.pathname);
    document.querySelectorAll("[data-asset-src]").forEach(function (el) {
      var basePath = el.getAttribute("data-asset-src");
      if (!basePath) return;
      el.setAttribute("src", inPages ? "../" + basePath : basePath);
    });
  }

  function currentPageName() {
    var path = window.location.pathname.replace(/\\/g, "/");
    var segs = path.split("/").filter(Boolean);
    var last = segs[segs.length - 1] || "";
    if (!last || last.indexOf(".") === -1) return "index.html";
    return last;
  }

  function setActiveNav() {
    var page = currentPageName();
    document.querySelectorAll(".site-nav a[data-nav]").forEach(function (a) {
      var href = a.getAttribute("href") || "";
      var target = href.split("/").pop() || "";
      if (page === target) {
        a.classList.add("is-active");
        a.setAttribute("aria-current", "page");
      }
    });
  }

  function initMobileNav() {
    var toggle = document.querySelector(".nav-toggle");
    var nav = document.getElementById("site-nav");
    var backdrop = document.getElementById("nav-backdrop");
    if (!toggle || !nav) return;

    function close() {
      nav.classList.remove("is-open");
      toggle.setAttribute("aria-expanded", "false");
      toggle.setAttribute("aria-label", "Open menu");
      document.body.classList.remove("nav-open");
      if (backdrop) {
        backdrop.hidden = true;
      }
    }

    function open() {
      nav.classList.add("is-open");
      toggle.setAttribute("aria-expanded", "true");
      toggle.setAttribute("aria-label", "Close menu");
      document.body.classList.add("nav-open");
      if (backdrop) {
        backdrop.hidden = false;
      }
    }

    toggle.addEventListener("click", function () {
      if (nav.classList.contains("is-open")) close();
      else open();
    });

    if (backdrop) {
      backdrop.addEventListener("click", close);
    }

    nav.querySelectorAll("a").forEach(function (link) {
      link.addEventListener("click", function () {
        if (window.matchMedia("(max-width: 768px)").matches) close();
      });
    });

    document.addEventListener("keydown", function (e) {
      if (e.key === "Escape") close();
    });
  }

  Promise.all([
    loadComponent("header", prefix + "css/components/header.html"),
    loadComponent("footer", prefix + "css/components/footer.html"),
  ])
    .then(function () {
      setAssetPaths();
      setNavHrefs();
      setActiveNav();
      initMobileNav();
    })
    .catch(function (err) {
      console.error(err);
    });
})();
