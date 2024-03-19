import React from 'react'
import "./GroupPageView.css"
import { useParams } from 'react-router-dom'
import useExtendedGroupInfo from './hooks/useExtendedGroupInfo'
import GroupHeader from './components/groupheader/GroupHeader'
import GroupMainContent from './components/groupmaincontent/GroupMainContent'

const GroupPageView = () => {
  let { id } = useParams();
  const groupInfo = useExtendedGroupInfo(id);
  return (
    <div className='GroupPageView'>
        <GroupHeader groupName={groupInfo?.Name}></GroupHeader>
        <GroupMainContent></GroupMainContent>
    </div>
  )
}

export default GroupPageView