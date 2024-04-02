import React from 'react'
import "./FooterFooter.css"
import CopyRightTextContainer from './components/copyrighttextcontainer/CopyRightTextContainer'
import TosContainer from './components/toscontainer/TosContainer'

const FooterFooter = () => {
  return (
    <div className='FooterFooter'>
      <CopyRightTextContainer></CopyRightTextContainer>
      <TosContainer></TosContainer>
    </div>
  )
}

export default FooterFooter