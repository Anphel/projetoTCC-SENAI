var geocoder; var geocoder;
var map;
var marker;

function initialize() {
    var latlng = new google.maps.LatLng(-25.495207903278086, -49.28979820000001);
    var options = {
        zoom: 5,
        center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    map = new google.maps.Map(document.getElementById("mapa"), options);

    geocoder = new google.maps.Geocoder();

    marker = new google.maps.Marker({
        map: map,
        draggable: true,
    });

    marker.setPosition(latlng);
}

$(document).ready(function () {

    initialize();

    function carregarNoMapa(endereco) {
        geocoder.geocode({ 'address': endereco + ', Brasil', 'region': 'BR' }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                if (results[0]) {
                    var latitude = results[0].geometry.location.lat();
                    var longitude = results[0].geometry.location.lng();

                    $('#txtEndereco').val(results[0].formatted_address);
                    $('#txtLatitude').val(latitude);
                    $('#txtLongitude').val(longitude);

                    var location = new google.maps.LatLng(latitude, longitude);
                    marker.setPosition(location);
                    map.setCenter(location);
                    map.setZoom(16);
                }
            }
        })
    }

    $("#btnEndereco").click(function () {
        if ($(this).val() != "")
            carregarNoMapa($("#txtEndereco").val());
    })

    $("#txtEndereco").blur(function () {
        if ($(this).val() != "")
            carregarNoMapa($(this).val());
    })

    google.maps.event.addListener(marker, 'drag', function () {
        geocoder.geocode({ 'latLng': marker.getPosition() }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                if (results[0]) {
                    $('#txtEndereco').val(results[0].formatted_address);
                    $('#txtLatitude').val(marker.getPosition().lat());
                    $('#txtLongitude').val(marker.getPosition().lng());

                    
                }
            }
        });
    });

    $("#txtEndereco").autocomplete({
        source: function (request, response) {
            geocoder.geocode({ 'address': request.term + ', Brasil', 'region': 'BR' }, function (results, status) {
                response($.map(results, function (item) {
                    return {
                        label: item.formatted_address,
                        value: item.formatted_address,
                        latitude: item.geometry.location.lat(),
                        longitude: item.geometry.location.lng()
                    }
                }));
            })
        },
        select: function (event, ui) {
            $("#txtLatitude").val(ui.item.latitude);
            $("#txtLongitude").val(ui.item.longitude);
            var location = new google.maps.LatLng(ui.item.latitude, ui.item.longitude);
            marker.setPosition(location);
            map.setCenter(location);
            map.setZoom(16);
        }
    });

    $("form").submit(function (event) {
        event.preventDefault();

        var endereco = $("#txtEndereco").val();
        var latitude = $("#txtLatitude").val();
        var longitude = $("#txtLongitude").val();

       
             document.getElementById('txtLatitude').innerText = latitude
             document.getElementById('txtLongitude').innerText = longitude
           

             //function getLatLong() {
             //var latitudeBD = document.getElementById("txtLatitude").value;
             //document.getElementById("hiddenLat").innerHTML = latitudeBD;

             //var longitudeBD = document.getElementById("txtLongitude").value;
             //document.getElementById("hiddenLong").innerHTML = longitudeBD;
             //}


             //document.getElementById("<%=txtLatitude.ClientID%>").innerText = 'Latitude: ' + '<%= Session["LatitudeBD"] %>';
             //document.getElementById("<%=txtLongitude.ClientID%>").innerText = 'Longitude: ' + '<%= Session["LongitudeBD"] %>';
             


        //alert("Endere�o: " + endereco + "\nLatitude: " + latitude + "\nLongitude: " + longitude);
    });

});

 $(".header").click(function () {

     $header = $(this);
     //getting the next element
     $content = $header.next();
     //open up the content needed - toggle the slide- if visible, slide up, if not slidedown.
     $content.slideToggle(500, function () {
         //execute this after slideToggle is done
         //change text of header based on visibility of content div
         $header.text(function () {
             //change text based on condition
             return $content.is(":visible") ? "Collapse" : "Expand";
         });
     });

 });
