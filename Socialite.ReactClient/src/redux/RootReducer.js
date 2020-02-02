import { combineReducers } from 'redux';
import { connectRouter } from 'connected-react-router';
import UserReducer from './reducers/UserReducer';
import PostReducer from './reducers/PostReducer';
import StatusReducer from './reducers/StatusReducer';

export default (history) => combineReducers({
    router: connectRouter(history),
    profile: combineReducers({
        user: UserReducer,
        status: StatusReducer,
        post: PostReducer,
    }),
});