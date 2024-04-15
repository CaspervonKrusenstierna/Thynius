import React from 'react'
import TextsViewPageView from './textsviewpageview/TextsViewPageView'
import { DashboardHeader } from "../../shared/components/dashboard"
import AssureLoggedIn from '../../shared/hooks/useAssureLoggedIn'

const TextsViewPage = () => {
  AssureLoggedIn();
  return (
    <>
        <DashboardHeader></DashboardHeader>
        <div className='flex flex-column justify-center w-[100vw]'>
          <TextsViewPageView></TextsViewPageView>
        </div>
    </>
  )
}

export default TextsViewPage