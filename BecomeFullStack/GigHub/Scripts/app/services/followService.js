var followService = function () {
    var follow = (followeeId, done, fail) => {

        $.post("/api/followings", { followeeId })
            .done(done)
            .fail(fail)
    }

    var unFollow = (followeeId, done, fail) => {
        $.ajax({
            url: "/api/followings/" + followeeId,
            method: "DELETE"
        })
            .done(done)
            .fail(fail)
    }


    return {
        follow,
        unFollow
    }
}();