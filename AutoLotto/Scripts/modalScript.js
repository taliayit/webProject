function toggleWorkoutModal_Layout(options) {
    console.log(options);
    //$("#layout_modal_title").text(options.time + " haha" + options.name);

    var imgpath = "../Content/images/exercises/" + options.name;
    $("#curr_exercise").attr("src", imgpath);

    $("#workoutModal").modal("toggle");
}



