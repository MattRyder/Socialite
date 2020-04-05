import SocialiteApi from '../SocialiteApi';

export const FETCH_USER_STATUSES_BEGIN = 'FETCH_USER_STATUSES_BEGIN';
export const FETCH_USER_STATUSES_SUCCESS = 'FETCH_USER_STATUSES_SUCCESS';
export const FETCH_USER_STATUSES_FAILURE = 'FETCH_USER_STATUSES_FAILURE';

export const fetchUserStatusesBegin = () => ({
    type: FETCH_USER_STATUSES_BEGIN,
});

export const fetchUserStatusesSuccess = (statuses) => ({
    type: FETCH_USER_STATUSES_SUCCESS,
    payload: { statuses },
});

export const fetchUserStatusesFailure = (error) => ({
    type: FETCH_USER_STATUSES_FAILURE,
    payload: { error },
});

function getSocialiteApi(accessToken) {
    return new SocialiteApi(
        process.env.REACT_APP_API_HOST,
        accessToken,
    );
}

export const fetchStatuses = (accessToken) => async (dispatch) => {
    const socialiteApi = getSocialiteApi(accessToken);

    dispatch(fetchUserStatusesBegin());

    try {
        const response = await socialiteApi.getStatuses();

        const { data } = response;

        dispatch(fetchUserStatusesSuccess(data));
    } catch (error) {
        dispatch(fetchUserStatusesFailure(error.message));
    }
};

export const createStatus = async (accessToken, statusParams) => {
    const socialiteApi = getSocialiteApi(accessToken);

    try {
        const response = await socialiteApi.createStatus(statusParams);
        debugger;
    } catch (error) {
        console.log(`Fuck: ${error}`);
    }
};
