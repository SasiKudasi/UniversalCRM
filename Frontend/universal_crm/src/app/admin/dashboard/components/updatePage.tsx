'use client'
import { api } from "@/lib/api";
import { useRouter } from "next/navigation";
import { useEffect, useState } from "react";
import { Button, Container, Form } from "react-bootstrap";
import { pageService } from '../services/pageService';
import { RootPage } from "../types/RootPage";

interface Page {
    id: string;
    title: string;
    path: string;
}

interface UpdatePageFormProps {
    pageId: string; // <-- сюда будем передавать id страницы
}

export const UpdatePageForm = ({ pageId }: UpdatePageFormProps) => {
    const router = useRouter();

    const [pages, setPages] = useState<Page[]>([]);

    useEffect(() => {
        api.get("/admin/all_page", {
            headers: { Accept: "application/json" },
            withCredentials: true,
        })
            .then(res => {
                setPages(res.data);
            })
            .catch(err => {
                console.error(err);
            });
    }, []);

    useEffect(() => {
        if (!pageId) return;

        api.get(`/admin/${pageId}`, {
            headers: { Accept: "application/json" },
            withCredentials: true,
        })
            .then((res) => setFormData(res.data))
            .catch((err) => {
                console.error(err);
                setError("Не удалось загрузить страницу");
            });
    }, [pageId]);



    const [formData, setFormData] = useState<RootPage | null>(null);

    const [parentPage, setParentPage] = useState('');
    const [error, setError] = useState<string | null>(null);
    const handleChange = (
        e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
    ) => {
        const { name, value, type, checked } = e.target as HTMLInputElement;

        setFormData((prev) =>
            prev
                ? {
                    ...prev,
                    [name]: type === "checkbox" ? checked : value,
                }
                : prev
        );
    };
    async function handleSubmit(e: React.FormEvent) {
        e.preventDefault();
        setError(null);

        try {
            if (!formData) {
                setError("Данные страницы не загружены");
                return;
            }
            const response = await pageService.createChild(parentPage, formData);
            console.log(response.status)
            if (response.status == 201) {
                router.push("/admin/dashboard/get_all_pages");
            }
        }
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        catch (error: any) {
            setError(error.message);
        }
    }

    if (!formData) return <p>Загрузка...</p>;

    return (
        <Container className="mt-4">
            <Form onSubmit={handleSubmit}>
                <Form.Select onChange={(e) => setParentPage(e.target.value)}>
                    {pages.map((page) => (
                        <option key={page.id} value={page.id}>
                            Имя: {page.title} | путь: {page.path}
                        </option>
                    ))}
                </Form.Select>
                <Form.Group className="mb-3">
                    <Form.Label>Title</Form.Label>
                    <Form.Control
                        type="text"
                        name="Title"
                        value={formData.Title}
                        required
                        onChange={handleChange}
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Path</Form.Label>
                    <Form.Control
                        type="text"
                        name="Path"
                        value={formData.Path}
                        required
                        onChange={handleChange}
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Порядковый номер</Form.Label>
                    <Form.Control
                        type="number"
                        name="OrdinalNum"
                        value={formData.OrdinalNum}
                        onChange={handleChange}
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Content</Form.Label>
                    <Form.Control
                        as="textarea"
                        rows={5}
                        name="Content"
                        value={formData.Content}
                        onChange={handleChange}
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Meta Title</Form.Label>
                    <Form.Control
                        type="text"
                        name="MetaTitle"
                        value={formData.MetaTitle}
                        onChange={handleChange}
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Meta Description</Form.Label>
                    <Form.Control
                        as="textarea"
                        rows={3}
                        name="MetaDescription"
                        value={formData.MetaDescription}
                        onChange={handleChange}
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Meta Keywords</Form.Label>
                    <Form.Control
                        type="text"
                        name="MetaKeywords"
                        value={formData.MetaKeywords}
                        onChange={handleChange}
                    />
                </Form.Group>

                <Form.Group className="mb-3" controlId="isActiveCheck">
                    <Form.Check
                        type="checkbox"
                        label="Активна?"
                        name="IsActive"
                        checked={formData.IsActive}
                        onChange={handleChange}
                    />
                </Form.Group>

                <Button type="submit" variant="primary">
                    Создать
                </Button>
                {error && <p style={{ color: "red" }}>{error}</p>}
            </Form>
        </Container>
    )
}