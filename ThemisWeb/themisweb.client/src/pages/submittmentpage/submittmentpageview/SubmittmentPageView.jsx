import React, { createContext } from 'react'
import useSubmittmentInfo from './hooks/useSubmittmentInfo';
import { useParams } from 'react-router-dom';
import SubmittmentSideMenu from './components/submittmentsidemenu/SubmittmentSideMenu';
import SubmittmentTextContainer from './components/submittmenttextcontainer/SubmittmentTextContainer';

export const submittmentInfoContext = createContext();

const SubmittmentPageView = () => {
  let { id } = useParams();
  let submittmentInfo = useSubmittmentInfo(id);

  return (
    <submittmentInfoContext.Provider value={submittmentInfo}>
      <div className='SubmittmentPageView'>
        <SubmittmentSideMenu></SubmittmentSideMenu>
        <SubmittmentTextContainer></SubmittmentTextContainer>
      </div>
    </submittmentInfoContext.Provider>
  )
}

export default SubmittmentPageView