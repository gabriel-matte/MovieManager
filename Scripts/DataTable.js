$(document).ready(function () {
    Table.Init();
});


var Table = {

    Data: [],
    Table: null,
    TableSelector: "#mediaTable",


    Init: function () {

        this.Data = $(this.TableSelector).data().tabledata;
        this.InitTable();

        $(window).resize(function () {
            Table.InitTable();
        });

        $(this.TableSelector).on('draw.dt', function () {

        });  
    },

    InitTable: function () {
        this.Table = $(this.TableSelector).DataTable({
            paging: true,
            ordering: true,
            order: [[0, "asc"]],
            retrieve: true,
            searching: true,
            bAutoWidth: false,
            columns: [
                {
                    render: function (data, type, full, meta) {
                        return data.Title != null ? data.Title : "";
                    },
                    data: function (row, type, val, meta) {
                        return row;
                    }
                },
                {
                    render: function (data, type, full, meta) {
                        return data.Type != null ? data.Type : "";
                    },
                    data: function (row, type, val, meta) {
                        return row;
                    }
                },
                {
                    render: function (data, type, full, meta) {
                        return data.ImdbRating != null ? data.ImdbRating.toFixed(1) : "";
                    },
                    data: function (row, type, val, meta) {
                        return row;
                    }
                },
                {
                    render: function (data, type, full, meta) {
                        return data.YearStart != null ? data.YearStart : "";
                    },
                    data: function (row, type, val, meta) {
                        return row;
                    }
                },
                {
                    render: function (data, type, full, meta) {
                        return data.YearEnd != null ? data.YearEnd : "";
                    },
                    data: function (row, type, val, meta) {
                        return row;
                    }
                },
                {
                    render: function (data, type, full, meta) {
                        var displayGenres = "";
                        if (data.Genre != null) {
                            for (i = 0; i < data.Genre.length; i++) {
                                displayGenres = displayGenres + data.Genre[i];
                                if (i != (data.Genre.length - 1)) {
                                    displayGenres = displayGenres + "<br />";
                                }
                            }
                        }
                        return displayGenres;
                    },
                    data: function (row, type, val, meta) {
                        return row;
                    }
                },
                {
                    render: function (data, type, full, meta) {
                        var displayStars = "";
                        if (data.Starring != null) {
                            for (i = 0; i < data.Starring.length; i++) {
                                displayStars = displayStars + data.Starring[i];
                                if (i != (data.Starring.length - 1)) {
                                    displayStars = displayStars + "<br />";
                                }
                            }
                        }
                        return displayStars;
                    },
                    data: function (row, type, val, meta) {
                        return row;
                    }
                },
                {
                    render: function (data, type, full, meta) {
                        var url;
                        if (data.ImdbLink == null) {
                            var title = encodeURIComponent(data.Title);
                            var imdbSearch = "https://www.imdb.com/find/?s=tt&q={title}&ref_=nv_sr_sm";
                            url = imdbSearch.replace("{title}", title);
                        } else {
                            url = data.ImdbLink;
                        }
                        var link = `<button class="btn btn-sm btn-warning my-auto" onclick="Table.GoToImdb('` + url + `')">
<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-link" style="margin-bottom:3px;" viewBox="0 0 16 16">
  <path d="M6.354 5.5H4a3 3 0 0 0 0 6h3a3 3 0 0 0 2.83-4H9c-.086 0-.17.01-.25.031A2 2 0 0 1 7 10.5H4a2 2 0 1 1 0-4h1.535c.218-.376.495-.714.82-1z"/>
  <path d="M9 5.5a3 3 0 0 0-2.83 4h1.098A2 2 0 0 1 9 6.5h3a2 2 0 1 1 0 4h-1.535a4.02 4.02 0 0 1-.82 1H12a3 3 0 1 0 0-6H9z"/>
</svg>
</button>
`;
                        return link;
                    },
                    data: function (row, type, val, meta) {
                        return row;
                    }
                },
                {
                    render: function (data, type, full, meta) {
                        var json = JSON.stringify(data);
                        var optionsButton = `<button type="button" class="btn btn-sm btn-primary" data-bs-toggle="modal" data-bs-target="#edit-media-modal" onclick='PopulateModal(\`` + json + `\`)'>
        Edit
    </button>
`;
                        return optionsButton;
                    },
                    data: function (row, type, val, meta) {
                        return row;
                    }
                }
            ],
            data: Table.Data
        })
    },

    SaveImportedData: function () {

        console.log("Saving new Media .................");

        var json = JSON.stringify(this.Data);

        document.querySelector("#json").value = json;

        document.querySelector("#saveForm").submit();

    },

    SaveAddData: function () {

        console.log("Saving ADDED Media .................");

        var json = JSON.stringify(this.Data);

        document.querySelector("#jsonAdd").value = json;

        document.querySelector("#addForm").submit();

    },

    GoToImdb: function (path) {
        window.open(path, '_blank').focus();
    },

    Search: function () {

        console.log(Table.Data);

    }
}

