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
                        return data.ImdbRating != null ? data.ImdbRating : "";
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

    Search: function () {

        console.log(Table.Data);

    }
}

