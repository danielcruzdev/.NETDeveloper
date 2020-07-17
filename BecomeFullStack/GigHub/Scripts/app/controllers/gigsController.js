var GigsController = function (attendanceService) {
    var button;

    var init = function () {
        $(".js-toggle-attendance").click(toggleAttendance);
    };


    const done = (button) => {
        var text = (button.text() == "Going") ? "Going?" : "Going"
        button.toggeClass("btn-info").toggeClass("btn-default").text(text);
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
