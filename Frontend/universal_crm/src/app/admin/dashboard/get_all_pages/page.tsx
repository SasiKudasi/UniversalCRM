'use client';
import { api } from "@/lib/api";
import { useEffect, useState } from "react";
import { ListGroup } from "react-bootstrap";
interface Page {
    id: number;
    title: string;
    path: string;
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
            <ListGroup>
                {pages.map((page) => (
                    <ListGroup.Item key={page.id}>
                        <strong>{page.path}</strong> — {page.title}
                    </ListGroup.Item>
                ))}
            </ListGroup>
        </div>
    );
}
