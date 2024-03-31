import React from 'react'
import "./TextsViewPageView.css"
import TextsHeader from './components/textsheader/TextsHeader';
import TextsContainer from './components/textscontainer/TextsContainer';
const TextsViewPageView = () => {


  return (
    <div className='TextsViewPageView'>
      <TextsHeader></TextsHeader>
      <TextsContainer></TextsContainer>
    </div>
  )
}

export default TextsViewPageView