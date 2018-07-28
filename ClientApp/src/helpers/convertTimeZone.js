export const convertTimeZone = (values) => {
    const val  = {...values, checkPoints: []}
        val.beginningOfStart = new Date(val.beginningOfStart.getTime() - val.beginningOfStart.getTimezoneOffset()*60000)
        val.endOfStart = new Date(val.endOfStart.getTime() - val.endOfStart.getTimezoneOffset()*60000)
        val.registerDeadline = new Date(val.registerDeadline.getTime() - val.registerDeadline.getTimezoneOffset()*60000)
        values.checkPoints.forEach((x, idx) => {
            let obj = {
                ...x, 
                open:new Date(values.checkPoints[idx].open.getTime() - values.checkPoints[idx].open.getTimezoneOffset()*60000),
                close: new Date(values.checkPoints[idx].close.getTime() - values.checkPoints[idx].close.getTimezoneOffset()*60000)
            }
            val.checkPoints = [...val.checkPoints, obj]
        });
        return val;
}