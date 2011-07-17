var userLocationUrl = "/LocationService.svc/userlocations";
function successFunction(position) {
    var lat = position.coords.latitude;
    var long = position.coords.longitude;
    /*public class UserLocation
    {
    [DataMember]
    public string UserName;
    [DataMember]
    public double Latitude;
    [DataMember]
    public double Longitude;
    }*/
    var userLocation = { UserName: usernametoreport, Latitude: lat, Longitude: long };
    var userLocationJSON = JSON.stringify(userLocation);
    $.ajax(
    {
        url: userLocationUrl,
        data: userLocationJSON,
        type: "POST",
        contentType: "application/json",
        dataType: "json",
        success: function (data) {
            alert(data);
        }
    });
    alert('Your latitude is :'+lat+' and longitude is '+long);
}
var usernametoreport =null;
function reportLocation(UserName) {
    if (navigator.geolocation&&UserName!=null) {
        usernametoreport = UserName;
        navigator.geolocation.getCurrentPosition(successFunction);
    }
}

function queryUserLocations(callback) {
    $.ajax({
        type: "GET",
        url: userLocationUrl,
        beforeSend: modifyHeader,
        success: function (data) {
            callback(data);
        }
    });
}
