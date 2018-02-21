function toggleWorkoutModal_Layout(options) {
    //$("#layout_modal_title").text(options.time + " haha" + options.name);
    console.log(options);
    var imgpath = "../Content/images/exercises/" + options.name;
    $("#curr_exercise").attr("src", imgpath);
    $("#workoutModal").modal("toggle");
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

    // $(".start").click(function () { $(".example.stopwatch").TimeCircles().start(); });
    //$(".stop").click(function () { $(".example.stopwatch").TimeCircles().stop(); });
    //$(".restart").click(function () { $(".example.stopwatch").TimeCircles().restart(); });
    //$(".start").click();
}



