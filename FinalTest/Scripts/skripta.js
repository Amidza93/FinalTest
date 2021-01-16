$(document).ready(function () {
    //podaci od interesa
    var host = window.location.host;
    var token = null;
    var headers = {};
    var employeesEndpoint = "/api/zaposleni/";

    loadEmployees();
    loadOrganizationalUnit();

    //skrivanje divova posto nisam inicijalno prijavljen
    $("#registration").css("display", "none");
    $("#signUp").css("display", "none");
    $("#info").css("display", "none");
    $("#formAddEmployee").css("display", "none");

    $("#btnSignUp").click(function (e) {
        e.preventDefault();
        $("#registration").css("display", "block");
        $("#picture1").css("display", "none");
    })

    $("#btnSignIn").click(function (e) {
        e.preventDefault();
        $("#signUp").css("display", "block");
        $("#picture1").css("display", "none");
    })

  

    //load organizational unit
    function loadOrganizationalUnit() {
        var requestUrl = 'https://' + host + "/api/jedinice";
        $.getJSON(requestUrl, setOrganizationalUnit);
    };

    //set organizational unit
    function setOrganizationalUnit(data, status) {
        if (status == "success") {
            var lista = $("#lista");

            for (var i = 0; i < data.length; i++) {
                var option = $("<option value=" + data[i].Id + ">" + data[i].Name + "</option>");
                lista.append(option);
            }
        } else {
            alert("greska prilikom ucitavanja liste")
        }
    }

    //load employees
    function loadEmployees() {
        var requestUrl = 'https://' + host + employeesEndpoint;
        $.getJSON(requestUrl, setEmployees);
    };

    //set employees
    function setEmployees(data, status) {
        var $container = $("#employees");
        $container.empty();

        if (status == "success") {
            // Caption
            var div = $("<div></div>");
            var h1 = $("<div class='row text-center'><h2>Zaposleni</h2></div>");
            $container.append(h1);
            // Table
            var table = $("<table class='table text-center'></table>");
            // Table header
            if (token) {
                var header = $("<thead style=\"background-color: yellow\"><tr><td>Ime i prezime</td><td>Rola</td><td>Godina zaposlenja</td><td>Godina rodjenja</td><td>Jedinica</td><td>Plata</td><td>Akcija</td></tr></thead>");
            } else {
                var header = $("<thead style=\"background-color: yellow\"><tr><td>Ime i prezime</td><td>Rola</td><td>Godina zaposlenja</td><td>Jedinica</td></tr></thead>");
            }
            table.append(header);
            // Table body
            tbody = $("<tbody></tbody>");
            for (i = 0; i < data.length; i++) {
                var row = "<tr>";

                var displayData = "<td>" + data[i].Name + "</td><td>" + data[i].Role + "</td><td>" + data[i].EmploymentYear + "</td><td>" + data[i].OrganizationalUnit.Name + "</td>";
                var displayDataSigned = "<td>" + data[i].Name + "</td><td>" + data[i].Role + "</td><td>" + data[i].EmploymentYear + "</td><td>" + data[i].BirthYear + "</td><td>" + data[i].OrganizationalUnit.Name + "</td><td>" + data[i].Sallary + "</td>";
               
                var stringId = data[i].Id.toString();
                var displayDelete = "<td><button id=btnDelete name=" + stringId + ">[Obrisi]</button></td>";

                if (token) {
                    row += displayDataSigned + displayDelete + "</tr>";
                } else {
                    row += displayData + "</tr>";
                }
                
                tbody.append(row);
            }
            table.append(tbody);

            div.append(table);
            if (token) {
                // prikaz forme ako je korisnik prijavljen
                $("#formAddEmployee").css("display", "block");
            }

            // ispis novog sadrzaja
            $container.append(div);
        }
        else {
            var div = $("<div></div>");
            var h1 = $("<h1>Greška prilikom preuzimanja zaposlenih!</h1>");
            div.append(h1);
            $container.append(div);
        }
    }

    //pretraga plate
    $("#btnSearch").click(function (e) {
        e.preventDefault();
        var start = $("#najmanje").val();
        var kraj = $("#najvise").val();

        //if (start < 251) {
        //    alert("Vrednost polja 'Najmanje' mora biti veca od 250.");
        //    return;
        //}

        //if (kraj >9999) {
        //    alert("Vrednost polja 'Najvise' mora biti manja od 10000.");
        //    return;
        //}

    
        //if ( start >= kraj) {
        //    alert("Nevalidna akcija!");
        //    $("#najmanje").val('');
        //    $("#najvise").val('');
        //    return;
        //}

        var sendData = {
            "Najmanje": start,
            "Najvise": kraj
        }
        if (token) {
            headers.Authorization = "Bearer " + token;
        }
        $.ajax({
            type: "POST",
            url: "https://" + host + "/api/pretraga",
            data: sendData,
            headers: headers
        })
            .done(function (data, status) {
                setEmployees(data, status);
            })
            .fail(function (data, status) {
                alert("Greska prilikom pretrage");
            })
    })

    //dodavanje zaposlenog
    $("#btnAdd").click(function (e) {
        e.preventDefault();

        var rola = $("#role").val();
        var ime = $("#name").val();
        var godinaRodj = $("#birthYear").val();
        var godinaZaposl = $("#empYear").val();
        var plata = $("#sallary").val();
        var orgJedinicaId = $("#lista option:selected").val();


      

        if (token) {
            headers.Authorization = "Bearer " + token;
        };
        sendData = {
            "Name": ime,
            "Role": rola,
            "BirthYear": godinaRodj,
            "EmploymentYear": godinaZaposl,
            "Sallary": plata,
            "OrganizationalUnitId": orgJedinicaId
        };

        $.ajax({
            type: "POST",
            data: sendData,
            url: "https://" + host + "/api/zaposleni",
            headers: headers
        })
            .done(function (data, status) {
                refreshTable();
            })
            .fail(function (data, status) {
                alert("Greska prilikom dodavanja!");
            })
    })

    // brisanje zaposlenog
    $("body").on("click", "#btnDelete", deleteEmployee);

    function deleteEmployee() {

        if (token) {
            headers.Authorization = "Bearer " + token;
        }

        var deleteId = this.name;


        $.ajax({
            type: "DELETE",
            url: "https://" + host + "/api/zaposleni/" + deleteId.toString(),
            headers: headers
        })
            .done(function (data, status) {
                refreshTable();
            })
    }

    //odustajanje registracija
    $("#btnGiveUp").click(function (e) {
        $("#registration").css("display", "none");
        $("#picture1").css("display", "block");
    })

    //odustajanje prijava
    $("#btnStop").click(function (e) {
        $("#signUp").css("display", "none");
        $("#picture1").css("display", "block");
    })

    //odustajanje forma za dodavanje
    $("#btnQuit").click(function (e) {
        $("#role").val('');
        $("#name").val('');
        $("#birthYear").val('');
        $("#empYear").val('');
        $("#sallary").val('');
        $('#lista option').prop('selected', function () {
            return this.defaultSelected;
        });
    })

    //registracija korisnika
    $("#btnRegistration").click(function (e) {
        e.preventDefault();

        var email = $("#registrationEmail").val();
        var loz1 = $("#registrationPassword").val();
        

        var sendData = {
            "Email": email,
            "Password": loz1,
            "ConfirmPassword": loz1
        };

        $.ajax({
            type: "POST",
            url: "https://" + host + "/api/Account/Register",
            data: sendData
        })
            .done(function (data) {
                $("#registration").css("display", "none");
                $("#signUp").css("display", "block");
            })
            .fail(function (data) {
                alert("Greska prilikom registracije!");
            });
    });

    //prijava korisnika
    $("#btnSign").click(function (e) {
        e.preventDefault();


        var email = $("#signUpEmail").val();
        var loz = $("#signUpPassword").val();
        var sendData = {
            "grant_type": "password",
            "username": email,
            "password": loz
        };

        $.ajax({
            "type": "POST",
            "url": "https://" + host + "/Token",
            "data": sendData
        })
            .done(function (data) {
                $("#info").css("display", "block");
                $("#infoUser").empty().append("Prijavljen korisnik: " + data.userName);
                token = data.access_token;
                $("#signUp").css("display", "none");


                refreshTable();
            })
            .fail(function (data) {
                alert("Greska prilikom prijave!");
            });
    });

    //odjava korisnika
    $("#btnSignOut").click(function () {
        token = null;
        headers = {};

        $("#registration").css("display", "none");
        $("#signUp").css("display", "none");
        $("#info").css("display", "none");
        $("#picture1").css("display", "block");
        $("#formAddEmployee").css("display", "none");
        $("#signUpEmail").val('');
        $("#signUpPassword").val('');
        $("#najmanje").val('');
        $("#najvise").val('');

        $('#lista option').prop('selected', function () {
            return this.defaultSelected;
        });

        refreshTable();
    })

    //refresh table
    function refreshTable() {
        $("#role").val('');
        $("#name").val('');
        $("#birthYear").val('');
        $("#empYear").val('');
        $("#sallary").val('');
        $('#lista option').prop('selected', function () {
            return this.defaultSelected;
        });

        loadEmployees();
    }



});