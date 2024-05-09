import React, { useRef, useState } from 'react'
import "./SubmitTextPageView.css"
import ChooseableAssignments from './chooseableassignments/ChooseableAssignments';
import { useParams } from 'react-router-dom';
import useFetch from '../../../shared/hooks/useFetch';
import { useTranslation } from 'react-i18next';

// could add search functionality but feels kinda unnecessary. How many assignments could u possibly have at once to justify a search filter?
const SubmitTextPageView = () => {
    const [t, i18n] = useTranslation();
    const [searchString, setSearchString] = useState();
    const {id} = useParams();

    function SubmitText(assignmentId){
      const response = useFetch("/usertext/submit?textId=" + id + "&assignmentId=" + assignmentId, "POST").then(j => j.json());
    }

  return (
    <div className='SubmitTextPageView'>
        <div className='SubmitTextPageView-Header'>
            <p className='SubmitTextPageView-HeaderTitle'>{t("Choose assignment")}</p>
        </div>
        <ChooseableAssignments onSelectAssignment={(id) => {SubmitText(id)}}></ChooseableAssignments>
    </div>
  )
}

export default SubmitTextPageView