function getTimeForUILonger(timeInMinutes) {
    if (!timeInMinutes) {
        return '-';
    }

    if (timeInMinutes === 60) {
        return 'up to 1 hour';
    }

    if (timeInMinutes >= 60) {
        let time = Math.floor(timeInMinutes / 60);
        return `${time} to ${time + 1} hours`;
    }

    return `less than ${timeInMinutes} minutes`;
}

function getTimeForUIShort(timeInMinutes) {
    if (!timeInMinutes) {
        return '0';
    }

    if (timeInMinutes === 60) {
        return '1 hr';
    }

    if (timeInMinutes >= 60) {
        let time = Math.floor(timeInMinutes / 60);
        return `${time} hrs`;
    }

    return `${timeInMinutes} mins`;
}

export { getTimeForUILonger, getTimeForUIShort };