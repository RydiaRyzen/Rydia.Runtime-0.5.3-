(() => {
    "use strict";

    const navItems = Array.from(
        document.querySelectorAll("[data-doc-target]")
    );

    const panels = Array.from(
        document.querySelectorAll(".docs-panel")
    );

    if (navItems.length === 0 || panels.length === 0) {
        return;
    }

    const panelIds = new Set(panels.map(panel => panel.id));

    function showDocument(id, updateHistory = true) {
        if (!panelIds.has(id)) {
            id = navItems[0].dataset.docTarget;
        }

        const target = document.getElementById(id);

        if (!target || !target.classList.contains("docs-panel")) {
            return;
        }

        // メインコンテンツの切り替え
        for (const panel of panels) {
            const active = panel.id === id;

            panel.hidden = !active;
            panel.classList.toggle("active", active);
        }

        // 左ペインの選択状態を更新
        for (const item of navItems) {
            const active = item.dataset.docTarget === id;

            item.classList.toggle("active", active);

            if (active) {
                item.setAttribute("aria-current", "page");
            } else {
                item.removeAttribute("aria-current");
            }
        }

        // 選択中の項目をURLに反映
        if (updateHistory && location.hash !== "#" + id) {
            history.pushState({ documentId: id }, "", "#" + id);
        }
    }

    for (const item of navItems) {
        item.addEventListener("click", () => {
            showDocument(item.dataset.docTarget);
        });
    }

    // ブラウザーの戻る・進むに対応
    window.addEventListener("popstate", () => {
        showDocument(location.hash.slice(1), false);
    });

    // URLのハッシュから初期ページを選択
    const initialId = decodeURIComponent(location.hash.slice(1));

    showDocument(
        initialId || navItems[0].dataset.docTarget,
        false
    );
})();