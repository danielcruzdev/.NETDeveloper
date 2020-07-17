var attendanceService = function () {
    var createAttendance = (gigId, done, fail) => {
        $.post("/api/attendances", { gigId })
            .done(done)
            .fail(fail)
    }

    var deleteAttendance = (gigId, done, fail) => {
        $.ajax({
            url: "/api/attendances/" + gigId,
            method: "DELETE"
        })
            .done(done)
            .fail(fail)
    }

    return {
        deleteAttendance,
        createAttendance
    }
}();