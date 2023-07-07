$(document).ready(function () {
    ConfigureAddModal();
});

var genreList = [];
var starringList = [];
var isEdit = false;

function ConfigureAddModal() {
    document.querySelector("#add-media-modal").addEventListener('hidden.bs.modal', function () {
        ResetModal();
    });
    document.querySelector("#addMediaForm").addEventListener('submit', ResetModal());
}

function PopulateModal(json) {

    $(document).ready(function () {
        var model = JSON.parse(json);

        console.log("SETTING MODAL INPUTS -------------");
        document.querySelector("#Title").value = model.Title;
        document.querySelector("#Title").setAttribute("readonly", "readonly");

        if (model.Type == null) {
            document.querySelector("#Type").selectedIndex = 0;
        } else if (model.Type.toLowerCase().includes("movie")) {
            document.querySelector("#Type").selectedIndex = 1;
        } else if (model.Type.toLowerCase().includes("series")) {
            document.querySelector("#Type").selectedIndex = 2;
        } else if (model.Type.toLowerCase().includes("doc")) {
            document.querySelector("#Type").selectedIndex = 3;
        }

        document.querySelector("#ImdbLink").value = model.ImdbLink;
        document.querySelector("#ImdbRating").value = model.ImdbRating;
        document.querySelector("#YearStart").value = model.YearStart;
        document.querySelector("#YearEnd").value = model.YearEnd;

        if (model.Genre != null) {
            model.Genre.forEach(function (g) {
                document.querySelector("#add-genre").value = g;
                AddGenre();
            });
        }
        if (model.Starring != null) {
            model.Starring.forEach(s => function () {
                document.querySelector("#add-starring").value = s;
                AddStarring();
            });
        }

        isEdit = true;
    });
    
}

function ResetModal() {
    console.log("RESETTING MODAL INPUTS -------------");
    document.querySelector("#Title").value = null;
    document.querySelector("#Type").selectedIndex = 0;
    document.querySelector("#ImdbLink").value = null;
    document.querySelector("#ImdbRating").value = null;
    document.querySelector("#YearStart").value = null;
    document.querySelector("#YearEnd").value = null;
    document.querySelector("#Genre").value = null;
    document.querySelector("#Starring").value = null;
    document.querySelector("#add-genre").value = null;
    document.querySelector("#add-starring").value = null;

    document.querySelector("#added-genres").innerHTML = "";
    document.querySelector("#added-starring").innerHTML = "";

    genreList = [];
    starringList = [];
}

function SearchImdb() {
    var title = document.querySelector("#Title").value;
    var imdbSearch = "https://www.imdb.com/find/?s=tt&q={title}&ref_=nv_sr_sm";

    var urlTitle = encodeURIComponent(title);

    imdbSearch = imdbSearch.replace("{title}", urlTitle);

    window.open(imdbSearch, '_blank').focus();
}

function AddGenre() {
    var input = document.querySelector("#add-genre");
    var labelContainer = document.querySelector("#added-genres");

    var pill = document.createElement("span");
    pill.classList.add("alert", "alert-secondary", "p-2", "mr-2", "me-2", "mt-3");

    var pillText = document.createElement("span");
    pillText.classList.add("temp-genre", "mt-3");
    pillText.innerHTML = document.querySelector("#add-genre").value;

    var closeBtn = document.createElement("span");
    closeBtn.type = "button";
    closeBtn.value = document.querySelector("#add-genre").value;
    closeBtn.classList.add("ms-2");
    closeBtn.setAttribute("data-bs-dismiss", "alert");
    closeBtn.setAttribute("onclick", "RemoveGenre(this)");
    closeBtn.innerHTML = `
<svg xmlns="http://www.w3.org/2000/svg" width="12" height="12" fill="currentColor" class="bi bi-x-lg mb-1" viewBox="0 0 16 16">
  <path d="M2.146 2.854a.5.5 0 1 1 .708-.708L8 7.293l5.146-5.147a.5.5 0 0 1 .708.708L8.707 8l5.147 5.146a.5.5 0 0 1-.708.708L8 8.707l-5.146 5.147a.5.5 0 0 1-.708-.708L7.293 8 2.146 2.854Z"/>
</svg>
`

    pill.append(pillText);
    pill.append(closeBtn);

    labelContainer.append(pill);

    /*genreList[document.querySelector("#add-genre").value] = document.querySelector("#add-genre").value;*/

    genreList.push(document.querySelector("#add-genre").value);

    document.querySelector("#add-genre").value = "";
}

function RemoveGenre(genre) {
    /*genreList[genre.value] = null;*/
    var index = genreList.indexOf(genre.value);
    if (index > -1) {
        genreList.splice(index, 1);
    }
}


function AddStarring() {
    var input = document.querySelector("#add-starring");
    var labelContainer = document.querySelector("#added-starring");

    var pill = document.createElement("span");
    pill.classList.add("alert", "alert-secondary", "mt-5", "p-2", "me-2");

    var pillText = document.createElement("span");
    pillText.classList.add("temp-starring");
    pillText.innerHTML = document.querySelector("#add-starring").value;

    var closeBtn = document.createElement("span");
    closeBtn.type = "button";
    closeBtn.value = document.querySelector("#add-starring").value;
    closeBtn.classList.add("ms-2");
    closeBtn.setAttribute("data-bs-dismiss", "alert");
    closeBtn.setAttribute("onclick", "RemoveStarring(this.value)");
    closeBtn.innerHTML = `
<svg xmlns="http://www.w3.org/2000/svg" width="12" height="12" fill="currentColor" class="bi bi-x-lg mb-1" viewBox="0 0 16 16">
  <path d="M2.146 2.854a.5.5 0 1 1 .708-.708L8 7.293l5.146-5.147a.5.5 0 0 1 .708.708L8.707 8l5.147 5.146a.5.5 0 0 1-.708.708L8 8.707l-5.146 5.147a.5.5 0 0 1-.708-.708L7.293 8 2.146 2.854Z"/>
</svg>
`

    pill.append(pillText);
    pill.append(closeBtn);

    labelContainer.append(pill);

    /*genreList[document.querySelector("#add-genre").value] = document.querySelector("#add-genre").value;*/

    starringList.push(document.querySelector("#add-starring").value);

    document.querySelector("#add-starring").value = "";
}

function RemoveStarring(starring) {
    /*genreList[genre.value] = null;*/
    var index = genreList.indexOf(starring);
    if (index > -1) {
        starringList.splice(index, 1);
    }
}

function SubmitModal() {

    document.querySelector("#Genre").value = JSON.stringify(genreList);
    document.querySelector("#Starring").value = JSON.stringify(starringList);

    var form = $("#addMediaForm")[0];

    var isValid = true;

    if (isValid) {
        form.submit();
    } else {
        return false;
    }

}