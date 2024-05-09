import useFetch from "../hooks/useFetch";

export async function setOrganizationTeachers(teachers){
    console.log(teachers);
    let formData = new FormData();
    if(teachers.length >= 1){
        formData.append("Teachers", teachers.map(s => s.id));
    }
    return fetch("/organization/organizationteachers", {
        method: "POST",
        body: formData
    });
}

export async function getOrganizationTeachers(){
    return useFetch("/organization/organizationteachers", "GET");
}