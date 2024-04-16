import DashboardNavItem from "./components/DashboardNavItem/DashboardNavItem"

export default function getDashboardHeaderNavItems(privilegeLevel){
    switch(privilegeLevel){
        case 3:
            return (
            <>
                <DashboardNavItem title="Hem" link="/dashboard/home"></DashboardNavItem>
                <DashboardNavItem title="Organizationer" link="/dashboard/organizations"></DashboardNavItem>
            </>)

          case 2:
            return (
            <>
                <DashboardNavItem title="Hem" link="/dashboard/home"></DashboardNavItem>
                <DashboardNavItem title="Lärare" link="/dashboard/teachers"></DashboardNavItem>
            </>)

          case 1:
           return (
            <>
                <DashboardNavItem title="Hem" link="/dashboard/home"></DashboardNavItem>
                <DashboardNavItem title="Grupper" link="/dashboard/groups"></DashboardNavItem>
            </>)
          default:
            return (
                <>
                    <DashboardNavItem title="Hem" link="/dashboard/home"></DashboardNavItem>
                    <DashboardNavItem title="Grupper" link="/dashboard/groups"></DashboardNavItem>
                    <DashboardNavItem title="Texter" link="/dashboard/texts"></DashboardNavItem>
                </>)
    }
}