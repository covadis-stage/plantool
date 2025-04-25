export const useUtil = () => {
    const formatDate = (date?: Date) => {
        if (!date) return '';
        return date.toLocaleDateString('nl-NL', {
            year: 'numeric',
            month: '2-digit',
            day: '2-digit',
        });
    };

    const getDateAsISOString = (date?: Date) => {
        if (!date) return '';

        let month = '' + (date.getMonth() + 1);
        let day = '' + date.getDate();
        let year = date.getFullYear();

        if (month.length < 2)
            month = '0' + month;
        if (day.length < 2)
            day = '0' + day;

        return [year, month, day].join('-');
    }

    return {
        formatDate,
        getDateAsISOString,
    }
}