function toggleWorkoutModal_Layout(options) {
    console.log(options);
    $("#layout_modal_title").text(options.time + " haha");
    $("#workoutModal").modal("toggle");
}