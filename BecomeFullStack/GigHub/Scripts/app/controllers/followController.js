var FollowController = function (followService) {
    var button;

    var init = () => {
        $(".js-toggle-follow").click(toggleFollow)
    }

    var done = () => {
        var text = (button.text() == "Follow") ? "Following" : "Follow"
        button.toggleClass("btn-info").toggleClass("btn-default").text(text);
    }

    var fail = () => {
        alert("Something Fail!")

    }

    var toggleFollow = (e) => {
        button = $(e.target);
        var followeeId = button.attr("data-user-id")

        if (button.hasClass("btn-default"))
            followService.follow(followeeId, done, fail)
        else 
            followService.unFollow(followeeId, done, fail)
    };

    return {
        init
    }

}(followService);

