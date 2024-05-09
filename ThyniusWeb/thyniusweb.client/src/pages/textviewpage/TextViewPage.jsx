import React from 'react'
import TextViewPageView from './textviewpageview/TextViewPageView'
import { DashboardHeader } from '../../shared/components/dashboard'
import AssureLoggedIn from '../../shared/hooks/useAssureLoggedIn'

const TextViewPage = () => {
  AssureLoggedIn();
  return (
    <>
      <DashboardHeader></DashboardHeader>
      <div className='flex flex-column justify-center w-[100vw]'>
      <TextViewPageView></TextViewPageView>
      </div>
    </>
  )
}

export default TextViewPage