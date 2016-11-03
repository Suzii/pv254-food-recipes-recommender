import fetch from 'isomorphic-fetch';
import * as AjaxUtils from './../utils/ajax';

export default function logUserActivity(objectToLog) {
    fetch(`/api/activityLog/RecommendationUsed`, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(objectToLog)
    }).then(AjaxUtils.processStatus)
        .then(AjaxUtils.parseJson)
        .then(r => console.log('Created', r))
        .catch(e => console.error('Log creation failed', e));
}