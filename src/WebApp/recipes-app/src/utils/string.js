function wrapSubstringInBoldTag(text, substring)
{
    return text.replace(new RegExp('(^|)(' + substring.trim() + ')(|$)','ig'), '$1<b>$2</b>$3');
}

export {
    wrapSubstringInBoldTag,
}
