import React from 'react'
import "./TextViewPageView.css"
import { useParams } from 'react-router-dom'
import TextRawContentContainer from './components/textrawcontentcontainer/TextRawContentContainer';
import TextFooter from './components/textfooter/TextFooter';


const TextViewPageView = () => {
  const {id} = useParams();
  return (
    <div className='TextViewPageView'>
      <TextRawContentContainer textId={id}></TextRawContentContainer>
      <TextFooter textId={id}></TextFooter>
    </div>
  )
}

export default TextViewPageView