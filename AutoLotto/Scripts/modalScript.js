function toggleWorkoutModal_Layout(options) {
    //$("#layout_modal_title").text(options.time + " haha" + options.name);

    var imgpath = "../Content/images/exercises/" + options.name;
    $("#curr_exercise").attr("src", imgpath);

    $("#workoutModal").modal("toggle");

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
    }, 300);
    
   // $(".start").click(function () { $(".example.stopwatch").TimeCircles().start(); });
    //$(".stop").click(function () { $(".example.stopwatch").TimeCircles().stop(); });
    //$(".restart").click(function () { $(".example.stopwatch").TimeCircles().restart(); });
    //$(".start").click();
}



