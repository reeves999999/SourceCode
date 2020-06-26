import 'bootstrap-3-typeahead';

const uri = 'api/v1/ClientApi/';

$(function () {
    $('#clientSearchForm input[name=search]').typeahead({
        hint: true,
        highlight: true,
        minLength: 2,
        source: function (query, process) {
            fetch(`${uri}Search/${query}`)
                .then(response => response.json())
                .then((data) => {
                    var parsed = data.map(function (x) {
                        var mapped = {...x}
                        return `${mapped.name}, ${mapped.webSite}, ${mapped.emailAddress}`
                    });

                    return process(parsed);
                });
        }
    });
});

