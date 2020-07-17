var GigsController = function (attendanceService) {
    var button;

    var init = function (container) {
        $(container).on("click", ".js-toggle-attendance", toggleAttendance)
    };


    const done = (button) => {
        var text = (button.text() == "Going") ? "Going?" : "Going"
        button.toggleClass("btn-info").toggleClass("btn-default").text(text);
    }

    const fail = () => {
        alert("Something failed!")
    }

    var toggleAttendance = (e) => {
        button = $(e.target);
        var gigId = button.attr("data-gig-id")

        if (button.hasClass("btn-default"))
            attendanceService.createAttendance(gigId, done, fail)

        else 
            attendanceService.deleteAttendance(gigId, done, fail)   
    }

    return {
        init
    }
}(attendanceService);
