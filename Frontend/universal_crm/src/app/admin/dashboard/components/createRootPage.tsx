'use client';
import { useRouter } from "next/navigation";
import { useState } from 'react';
import { Button, Container, Form } from 'react-bootstrap';
import { pageService } from '../services/pageService';
import { RootPage } from "../types/RootPage";

export const CreateRootPageForm = () => {
    const router = useRouter();

    const [formData, setFormData] = useState<RootPage>({
        title: '',
        path: '',
        ordinalNuber: 0,
        content: '',
        metaTitle: '',
        metaDescription: '',
        metaKeywords: '',
        isActive: true
    });
    const [error, setError] = useState<string | null>(null);
    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const { name, value, type } = e.target;

        setFormData(prev => ({
            ...prev,
            [name]: type === 'checkbox' ? (e.target as HTMLInputElement).checked : value
        }));
    };
    async function handleSubmit(e: React.FormEvent) {
        e.preventDefault();
        setError(null);

        try {
            const response = await pageService.createRoot(formData);
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

    return (

            <Form onSubmit={handleSubmit}>
                <Form.Group className="mb-3">
                    <Form.Label>Title</Form.Label>
                    <Form.Control
                        type="text"
                        name="title"
                        value={formData.title}
                        required
                        onChange={handleChange}
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Path</Form.Label>
                    <Form.Control
                        type="text"
                        name="path"
                        value={formData.path}
                        required
                        onChange={handleChange}
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Порядковый номер</Form.Label>
                    <Form.Control
                        type="number"
                        name="ordinalNuber"
                        value={formData.ordinalNuber}
                        onChange={handleChange}
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Content</Form.Label>
                    <Form.Control
                        as="textarea"
                        rows={5}
                        name="content"
                        value={formData.content}
                        onChange={handleChange}
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Meta Title</Form.Label>
                    <Form.Control
                        type="text"
                        name="metaTitle"
                        value={formData.metaTitle}
                        onChange={handleChange}
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Meta Description</Form.Label>
                    <Form.Control
                        as="textarea"
                        rows={3}
                        name="metaDescription"
                        value={formData.metaDescription}
                        onChange={handleChange}
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Meta Keywords</Form.Label>
                    <Form.Control
                        type="text"
                        name="metaKeywords"
                        value={formData.metaKeywords}
                        onChange={handleChange}
                    />
                </Form.Group>

                <Form.Group className="mb-3" controlId="isActiveCheck">
                    <Form.Check
                        type="checkbox"
                        label="Активна?"
                        name="isActive"
                        checked={formData.isActive}
                        onChange={handleChange}
                    />
                </Form.Group>

                <Button type="submit" variant="primary">
                    Создать
                </Button>
                {error && <p style={{ color: "red" }}>{error}</p>}
            </Form>
    );
}