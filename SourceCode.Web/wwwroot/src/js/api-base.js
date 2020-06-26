const apiModule = () => {

    return get = (url) => {
        return fetch(`/api/v1/${url}`);
    }

}

export default apiModule;