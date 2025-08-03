'use client';
import { api } from "@/lib/api";
import { useEffect, useState } from "react";

interface Page {
    id: number;
    title: string;
}


export default function GetAllPages() {
    const [pages, setPages] = useState<Page[]>([]);

    useEffect(() => {
        api.get("/admin/all_page", {
            headers: { Accept: "application/json" },
            withCredentials: true,
        })
            .then(res => {
                setPages(res.data);
            })
            .catch(err => console.error(err));
    }, []);

    if (!pages) return <p>Загрузка...</p>;

    return (
        <div>
            <h1>Страницы:</h1>
            <ul>
                {pages.map(page => (
                    <li key={page.id}>{page.title}</li>
                ))}
            </ul>
        </div>
    );
}
