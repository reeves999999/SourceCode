import apiModule from './api-base.js';
import * as $ from 'jquery';
import 'bootstrap-3-typeahead';


    $(function () {
        $('#clientSearchForm input[name=search]').typeahead({
            hint: true,
            highlight: true,
            minLength: 2,
            source: function (query, process) {
                apiModule.get(`ClientsApi/Search/${query}`)
                    .then(response => response.json())
                    .then((data) => {
                    var parsed = data.map(function (x) {
                        return `${x.name}, ${x.webSite}`
                    });

                    return process(parsed);
                });
            }
        });
    });

