function myFunction() {
    var dots = document.getElementById("dots");
    var moreText = document.getElementById("more");
    var btnText = document.getElementById("myBtn");

    if (dots.style.display === "none") {
        dots.style.display = "inline";
        btnText.innerHTML = "View Query";
        moreText.style.display = "none";
    } else {
        dots.style.display = "none";
        btnText.innerHTML = "view less";
        moreText.style.display = "inline";
    }
}

function myFunction1() {
    var ansicon = document.getElementById("ansicon");
    var moreanswersText = document.getElementById("moreanswers");
    var ansbtnText = document.getElementById("ansBtn");

    if (ansicon.style.display === "none") {
        ansicon.style.display = "inline";
        ansbtnText.innerHTML = "View More Answers";
        moreanswersText.style.display = "none";
    } else {
        ansicon.style.display = "none";
        ansbtnText.innerHTML = "view less Answers";
        moreanswersText.style.display = "inline";
    }
}