import React from 'react';
import { useParams } from 'react-router';
import UserProfileFormComponent from '../../../../../UserProfile/UserProfileForm/UserProfileFormComponent';
import { useTranslations, useUser, useStateSelector } from '../../../../../../hooks';

import './UserProfilePaneEdit.scss';

export default function UserProfilePaneEdit() {
    const { t, TextTranslationKeys } = useTranslations();

    const { id } = useParams();

    const { userId } = useUser();

    const userProfile = useStateSelector(userId, 'userProfile', 'userProfiles', id);

    return (
        <div className="user-profile-pane-edit">
            <div className="user-profile-header">
                <span>{t(TextTranslationKeys.settingsView.userProfiles.edit)}</span>
            </div>

            <UserProfileFormComponent userProfile={userProfile} />
        </div>
    );
}
