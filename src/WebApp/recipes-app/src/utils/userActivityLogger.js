import fetch from 'isomorphic-fetch';
import * as AjaxUtils from './../utils/ajax';

export default function logUserActivity(displayedRecipeId, clickedRecipeId, recommendedBy) {

    const objectToLog = {
        displayedRecipeId,
        clickedRecipeId,
        recommendedBy,
        timestamp: new Date().toISOString()
    };

    fetch(`/api/activityLog/RecommendationUsed`, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(objectToLog)
    }).then(AjaxUtils.processStatus)
        .then(AjaxUtils.parseJson)
        .then(r => r)
        .catch(e => console.error('Log creation failed', e));
}