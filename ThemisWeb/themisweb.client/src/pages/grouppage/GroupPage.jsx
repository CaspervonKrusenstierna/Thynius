import React, { useContext } from 'react'
import useExtendedGroupInfo from './hooks/useExtendedGroupInfo';
import "./GroupPage.css"
import GroupHeader from './components/groupheader/GroupHeader';
import GroupMainContent from './components/groupmaincontent/GroupMainContent';

const GroupPage = () => {
  const sessionInfo = useContext(sessionInfoContext);
  const groupInfo = useExtendedGroupInfo()

  return (
    <div className='Page'>
        <GroupHeader Name={groupInfo.Name} DateCreated={groupInfo.DateCreated}></GroupHeader>
        <GroupMainContent AssignmentInfo={groupInfo.AssignmentInfo} MemberInfo={groupInfo.MemberInfo}></GroupMainContent>
    </div>
  )
}

export default GroupPage