window.triggerFileInput = (id) => {
    const el = document.getElementById(id);

    console.log("input trouvé:", el);

    if (el) {
        el.click();
    } else {
        console.error("INPUT NON TROUVÉ");
    }
};