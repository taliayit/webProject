﻿function toggleWorkoutModal_Layout(options) {
    /* Get iframe src attribute value i.e. YouTube video url
    and store it in a variable */
    //var url = $("#exVideo").attr('src');
    var url = options.url;
    videoGlobalUrl = url;

    /* Assign empty url value to the iframe src attribute when
    modal hide, which stop the video playing */
    $("#workoutModal").on('hide.bs.modal', function () {
        $("#exVideo").attr('src', '');
        $("#modal-title").text('');
    });

    /* Assign the initially stored url back to the iframe src
    attribute when modal is displayed again */
    $("#workoutModal").on('show.bs.modal', function () {
        $("#exVideo").attr('src', url);
        var ytApiKey = "AIzaSyCJr4RljbMoH8kBGt3srhzxNCI56vsUA78";
        var videoId = url.split("/");
        videoId = videoId[videoId.length - 1];

        $.get("https://www.googleapis.com/youtube/v3/videos?part=snippet&id=" + videoId + "&key=" + ytApiKey, function (data) {
            $("#modal-title").text(data.items[0].snippet.title);
        });
    });
    console.log(options);

    $("#workoutModal").modal("toggle");
    /*
    var imgpath = "../Content/images/exercises/" + options.name;
    $("#curr_exercise").attr("src", imgpath);
    
   setTimeout(function () {
       //$("#workoutModal").find('.my-flipster').flipster({
       //});

       $("#workoutModal").find('.flexslider').flexslider({
           animation: "slide",
           controlNav: "thumbnails",
           slideshowSpeed: options.time*10
       });

       //$("#workoutModal").find("#carousel").slidingCarousel({
       //    squeeze: 100,
       //    animate: 250,
       //    shadow: true
       //});
   }, 300);
    
   setTimeout(function () {
       $("#workoutModal").find(".modalStopwatch").TimeCircles({
           start: true, count_past_zero: false,
           time: {
               Days: { show: false },
               Hours: { show: false },
               Minutes: { show: false },
               Seconds: { show: true, color: "#c55" }
           },
           total_duration: 90
       });
       $("#workoutModal").find(".modalStopwatch").attr("data-timer", options.time * 10)
   }, 300);
   */
    //$(".start").click(function () { $(".example.stopwatch").TimeCircles().start(); });
    //$(".stop").click(function () { $(".example.stopwatch").TimeCircles().stop(); });
    //$(".restart").click(function () { $(".example.stopwatch").TimeCircles().restart(); });
    //$(".start").click();
}


function run_example() {

    var example_item = { 'img': '../Content/images/facebook-like.svg', 'info': 'Hello world!' };
    $('#workoutModal').barrager(example_item);
    return false;

}