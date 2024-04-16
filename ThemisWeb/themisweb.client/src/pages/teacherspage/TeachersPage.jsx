import React from 'react'
import { DashboardHeader } from '../../shared/components/dashboard'
import TeachersPageView from './teacherspageview/TeachersPageView'

const OrganizationsPage = () => {
    return (
        <>
            <DashboardHeader></DashboardHeader>
            <div className='flex flex-column justify-center w-[100vw]'>
                <TeachersPageView></TeachersPageView>
            </div>
        </>
      )
}

export default OrganizationsPage