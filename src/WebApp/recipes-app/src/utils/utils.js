function getTimeForUI(timeInMinutes) {
    if (timeInMinutes === 60) {
        return 'up to 1 hour';
    }

    if (timeInMinutes >= 60) {
        let time = Math.floor(timeInMinutes / 60);
        return `${time} to ${time + 1} hours`;
    }

    return `less than ${timeInMinutes} minutes`;
}

export { getTimeForUI };